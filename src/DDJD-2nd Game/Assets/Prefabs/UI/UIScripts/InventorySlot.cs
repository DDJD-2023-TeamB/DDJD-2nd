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
    InventoryUI inventoryUI;
    public GameObject ItemTitleTextPrefab;
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
        GameObject iui = GameObject.Find("InventoryUI");
        if (iui != null)
        {
            inventoryUI = iui.GetComponent<InventoryUI>();
            if (inventoryUI == null)
            {
                Debug.LogError("InventoryUI Component not found in UI object");
            }
        }
        else
        {
            Debug.LogError("Couldn't find InevntoryUI object in InventorySlot.css");
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            currentItem = dropped.GetComponent<InventoryItemImage>().currentItem;
            OnDropAction?.Invoke(currentItem, index);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(currentItem == null)
        {
            return;
        }
        if (currentItem.item != null && !Input.GetMouseButton(0))
        {
            if (inventoryUI.itemTitle != null)
            {
                Destroy(inventoryUI.itemTitle);
                inventoryUI.itemTitle = null;
            }
            inventoryUI.itemTitle = Instantiate(
                ItemTitleTextPrefab,
                Input.mousePosition + new Vector3(0, 5, 0),
                Quaternion.identity
            );
            TextMeshProUGUI itemTitle = inventoryUI.itemTitle.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            itemTitle.text = currentItem.item.Name;
            TextMeshProUGUI itemDescription = inventoryUI.itemTitle.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            itemDescription.text = currentItem.item.Description;
            RectTransform backgroundTint = inventoryUI.itemTitle.transform.GetChild(0).GetComponent<RectTransform>();
            //Setting size and position of dark background
            backgroundTint.anchoredPosition = new Vector2(0, itemTitle.preferredHeight+10);
            Debug.Log(itemTitle.preferredHeight + "," + itemDescription.preferredHeight);
            backgroundTint.sizeDelta = new Vector2(200, itemTitle.preferredHeight + itemDescription.preferredHeight+20);
            inventoryUI.itemTitle.transform.SetParent(transform.root);
            _graphic.raycastTarget = false;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (inventoryUI.itemTitle != null)
        {
            Destroy(inventoryUI.itemTitle);
            inventoryUI.itemTitle = null;
        }
    }

    private void Update()
    {
        if (inventoryUI.itemTitle != null)
        {
            inventoryUI.itemTitle.transform.position = Input.mousePosition + new Vector3(0, 10, 0);
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (inventoryUI.itemTitle != null)
            {
                Destroy(inventoryUI.itemTitle);
                inventoryUI.itemTitle = null;
            }
        }
    }
}
