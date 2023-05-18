using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static MyBox.EditorTools.MyGUI;

public class InventorySlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    GameObject UIObject;
    public GameObject ItemTitleTextPrefab;
    GameObject currentTitleTextObject = null;
    public ItemStack currentItem;
    public Boolean isFromWheel = false; //Wether this slot belongs to a spell wheel, set in the inspector
    public int wheelIndex = -1;
    public string wheelSide = "";
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

            //Check if old slot is a wheel slot, if so update wheel
            string parentName = dropped.GetComponent<InventoryItemImage>().parentAfterDrag.name;
            if (parentName.Contains("SpellSlot"))
            {
                if (dropped.GetComponent<InventoryItemImage>().parentAfterDrag.parent.name == "LeftSpellWheel")
                {
                    UIObject.GetComponent<UIController>().ChangeLeftWheelItem(int.Parse(parentName[parentName.Length-1].ToString()), null);
                }
                else if(dropped.GetComponent<InventoryItemImage>().parentAfterDrag.parent.name == "RightSpellWheel")
                {
                    UIObject.GetComponent<UIController>().ChangeRightWheelItem(int.Parse(parentName[parentName.Length - 1].ToString()), null);

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
                    UIObject.GetComponent<UIController>().ChangeLeftWheelItem(wheelIndex,currentItem);
                }else if (wheelSide == "right")
                {
                    UIObject.GetComponent<UIController>().ChangeRightWheelItem(wheelIndex, currentItem);
                }
                else
                {
                    Debug.LogError("Wheel slot has an invalid side!");
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (currentItem != null)
        {
            currentTitleTextObject = Instantiate(ItemTitleTextPrefab, Input.mousePosition + new Vector3(0,5,0), Quaternion.identity);
            currentTitleTextObject.GetComponent<TextMeshProUGUI>().text = currentItem.type.name;
            currentTitleTextObject.transform.SetParent(transform.root);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (currentTitleTextObject != null)
        {
            Destroy(currentTitleTextObject);
            currentTitleTextObject = null;
        }
    }

    private void Update()
    {
        if(currentTitleTextObject != null)
        {
            currentTitleTextObject.transform.position = Input.mousePosition + new Vector3(0, 10, 0);
        }
    }
}







