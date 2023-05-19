using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemImage
    : MonoBehaviour,
        IBeginDragHandler,
        IDragHandler,
        IEndDragHandler,
        IPointerClickHandler
{
    public Image image;
    public ItemStack currentItem;
    public GameObject inventoryUI;
    public GameObject itemAmountText;

    [HideInInspector]
    public Transform parentAfterDrag;

    private void Start()
    {
        inventoryUI = GameObject.Find("InventoryUI");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
        itemAmountText.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
        itemAmountText.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //TODO:: Check if it's consumable
        if (eventData.button == PointerEventData.InputButton.Right && false)
        {
            //Apply effect
            inventoryUI.GetComponent<InventoryUI>().RemoveItem(this);
        }
    }
}
