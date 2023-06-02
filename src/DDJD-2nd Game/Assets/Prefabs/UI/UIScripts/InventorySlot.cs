using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static MyBox.EditorTools.MyGUI;

public class InventorySlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    GameObject UIObject;
    public GameObject ItemTitleTextPrefab;
    GameObject currentTitleTextObject = null;
    private int index;
    public int Index
    {
        get { return index; }
        set { index = value; }
    }

    public ItemStack currentItem;

    public Boolean isFromWheel = false; //Wether this slot belongs to a spell wheel, set in the inspector
    public int wheelIndex = -1;
    public string wheelSide = "";

    private Graphic _graphic;

    public Action<ItemStack, int> OnDropAction;

    private void Awake()
    {
        _graphic = GetComponent<Graphic>();
    }

    private void Start()
    {
        UIObject = GameObject.Find("UI");
        if (UIObject == null)
        {
            Debug.LogError("InventoryUI not found!");
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            currentItem = dropped.GetComponent<InventoryItemImage>().currentItem;
            Debug.Log("Drop");
            OnDropAction?.Invoke(currentItem, index);
            /*
            //Check if old slot is a wheel slot, if so update wheel
            string parentName = dropped.GetComponent<InventoryItemImage>().parentAfterDrag.name;
            if (parentName.Contains("SpellSlot"))
            {
                if (
                    dropped.GetComponent<InventoryItemImage>().parentAfterDrag.parent.name
                    == "LeftSpellWheel"
                )
                {
                    UIObject
                        .GetComponent<UIController>()
                        .ChangeLeftWheelItem(
                            int.Parse(parentName[parentName.Length - 1].ToString()),
                            currentItem
                        );
                }
                else if (
                    dropped.GetComponent<InventoryItemImage>().parentAfterDrag.parent.name
                    == "RightSpellWheel"
                )
                {
                    UIObject
                        .GetComponent<Player>()
                        .UIController.ChangeRightWheelItem(
                            int.Parse(parentName[parentName.Length - 1].ToString()),
                            null
                        );
                }
                else
                {
                    Debug.LogError("Invalid parent name of spell slot!!!");
                }
            }

            dropped.GetComponent<InventoryItemImage>().parentAfterDrag = transform;
            if (isFromWheel)
            {
                if (wheelSide == "left")
                {
                    UIObject
                        .GetComponent<UIController>()
                        .ChangeLeftWheelItem(wheelIndex, currentItem.type);
                }
                else if (wheelSide == "right")
                {
                    UIObject
                        .GetComponent<UIController>()
                        .ChangeRightWheelItem(wheelIndex, currentItem.type);
                }
                else
                {
                    Debug.LogError("Wheel slot has an invalid side!");
                }
            }
            */
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ItemTitleTextPrefab == null)
        {
            return;
        }
        if (currentItem != null)
        {
            currentTitleTextObject = Instantiate(
                ItemTitleTextPrefab,
                Input.mousePosition + new Vector3(0, 5, 0),
                Quaternion.identity
            );
            currentTitleTextObject.GetComponent<TextMeshProUGUI>().text = currentItem.item.name;
            currentTitleTextObject.transform.SetParent(transform.root);
            _graphic.raycastTarget = false;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (currentTitleTextObject != null)
        {
            Destroy(currentTitleTextObject);
            currentTitleTextObject = null;
            //_graphic.raycastTarget = true;
        }
    }

    private void Update()
    {
        if (currentTitleTextObject != null)
        {
            currentTitleTextObject.transform.position = Input.mousePosition + new Vector3(0, 10, 0);
        }
    }
}
