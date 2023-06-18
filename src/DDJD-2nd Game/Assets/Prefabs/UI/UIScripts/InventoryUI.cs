using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.Collections.LowLevel.Unsafe;

public enum UiArea
{
    Items,
    Spells,
    LeftWheel,
    RightWheel
}

public class InventoryUI : MonoBehaviour
{
    public GameObject InventoryItemPrefab;

    public GameObject itemsPanel;
    public GameObject spellsPanel;

    [SerializeField]
    private GameObject _leftWheel;

    [SerializeField]
    private GameObject _rightWheel;

    private Player _player;

    public void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public Action OnItemRemoved;
    public Action<InventoryItemImage, int, UiArea> OnItemDrop;
    public Action<InventoryItemImage, int, UiArea> OnItemSkillDrop;
    public Action<InventoryItemImage, int, UiArea> OnItemSkillLeftDrop;
    public Action<InventoryItemImage, int, UiArea> OnItemSkillRightDrop;

    public GameObject itemTitle;

    public void SetupActions()
    {
        for (int i = 0; i < itemsPanel.transform.childCount; i++)
        {
            InventorySlot slot = itemsPanel.transform.GetChild(i).GetComponent<InventorySlot>();
            if (slot != null)
            {
                slot.Index = i;
                slot.OnDropAction += OnItemDrop;
            }
        }
        for (int i = 0; i < spellsPanel.transform.childCount; i++)
        {
            InventorySlot slot = spellsPanel.transform.GetChild(i).GetComponent<InventorySlot>();
            if (slot != null)
            {
                slot.Index = i;
                slot.OnDropAction += OnItemSkillDrop;
            }
        }
        for (int i = 0; i < _leftWheel.transform.childCount; i++)
        {
            InventorySlot slot = _leftWheel.transform.GetChild(i).GetComponent<InventorySlot>();
            if (slot != null)
            {
                slot.Index = i;
                slot.OnDropAction += OnItemSkillLeftDrop;
            }
        }

        for (int i = 0; i < _rightWheel.transform.childCount; i++)
        {
            InventorySlot slot = _rightWheel.transform.GetChild(i).GetComponent<InventorySlot>();
            if (slot != null)
            {
                slot.Index = i;
                slot.OnDropAction += OnItemSkillRightDrop;
            }
        }
    }

    public Transform FindAvailableItemSlot()
    {
        for (int i = 0; i < itemsPanel.transform.childCount; i++)
        {
            Transform slot = itemsPanel.transform.GetChild(i);
            InventorySlot slotController = slot.GetComponent<InventorySlot>();
            if (slotController.currentItem == null)
            {
                return slot;
            }
        }
        return null;
    }

    public Transform FindAvailableSpellSlot()
    {
        for (int i = 0; i < spellsPanel.transform.childCount; i++)
        {
            if (spellsPanel.transform.GetChild(i).childCount == 0)
            {
                return spellsPanel.transform.GetChild(i);
            }
        }
        return null;
    }

    public Transform GetLeftSkillSlot(int index)
    {
        return _leftWheel.transform.GetChild(index);
    }

    public Transform GetRightSkillSlot(int index)
    {
        return _rightWheel.transform.GetChild(index);
    }

    //Finds an available slot and adds an item to that slot. If no available slots left, returns false, if successful returns true
    public bool AddItem(ItemStack item, UiArea area)
    {
        Transform availableSlot;
        if (item.item is ItemSkill)
        {
            availableSlot = FindAvailableSpellSlot();
        }
        else
        {
            availableSlot = FindAvailableItemSlot();
        }
        if (availableSlot == null)
        {
            return false;
        }
        GameObject newItem = AddItemToSlot(item, availableSlot, area);
        return true;
    }

    //Goes through all slots and removes their items
    public void RemoveAllItems()
    {
        for (int i = 0; i < itemsPanel.transform.childCount; i++)
        {
            Transform slot = itemsPanel.transform.GetChild(i);
            RemoveItem(slot);
        }
    }

    //Returns the slot where an item is. If the item is not found, returns null
    public Transform FindItem(ItemStack item)
    {
        if (item.item is ItemSkill)
        {
            for (int i = 0; i < spellsPanel.transform.childCount; i++)
            {
                if (spellsPanel.transform.GetChild(i).childCount != 0)
                {
                    Transform slot = spellsPanel.transform.GetChild(i);
                    if (slot.GetChild(0).GetComponent<InventoryItemImage>().currentItem == item)
                    {
                        return slot;
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < itemsPanel.transform.childCount; i++)
            {
                if (itemsPanel.transform.GetChild(i).childCount != 0)
                {
                    Transform slot = itemsPanel.transform.GetChild(i);
                    if (slot.GetChild(0).GetComponent<InventoryItemImage>().currentItem == item)
                    {
                        return slot;
                    }
                }
            }
        }
        return null;
    }

    //Looks for an item and removes it from the slot
    public bool RemoveItem(ItemStack item)
    {
        Transform slot = FindItem(item);
        if (slot != null)
        {
            RemoveItem(slot);
            return true;
        }
        return false;
    }

    //Removes an item from the specified slot
    public bool RemoveItem(Transform slot)
    {
        if (slot.childCount == 0)
        {
            InventorySlot slotController = slot.GetComponent<InventorySlot>();
            if (slotController.currentItem != null)
            {
                slotController.currentItem = null;
                return true;
            }
            return false;
        }
        //Debug.Log("Deleting: " + slot.name + " with count " + slot.childCount);
        Destroy(slot.GetChild(0).gameObject);
        slot.GetComponent<InventorySlot>().currentItem = null;
        return true;
    }

    public void SetLeftWheelSkills(List<ItemSkill> skills)
    {
        for (int i = 0; i < skills.Count; i++)
        {
            Transform slot = GetLeftSkillSlot(i);
            InventorySlot slotController = slot.GetComponent<InventorySlot>();
            slotController.ClearSlot();
            if (skills[i] == null)
            {
                continue;
            }

            //Remove children
            ItemStack item = new ItemStack(skills[i], 1, null);
            GameObject newItem = AddItemToSlot(item, slot, UiArea.LeftWheel);
        }
    }

    public void SetRightWheelSkills(List<ItemSkill> skills)
    {
        for (int i = 0; i < skills.Count; i++)
        {
            Transform slot = GetRightSkillSlot(i);
            InventorySlot slotController = slot.GetComponent<InventorySlot>();
            slotController.ClearSlot();
            if (skills[i] == null)
            {
                continue;
            }
            ItemStack item = new ItemStack(skills[i], 1, null);
            GameObject newItem = AddItemToSlot(item, slot, UiArea.RightWheel);
        }
    }

    private GameObject AddItemToSlot(ItemStack itemStack, Transform slot, UiArea area)
    {
        InventorySlot invSlot = slot.GetComponent<InventorySlot>();
        if (invSlot.currentItem != null)
        {
            RemoveItem(slot);
        }
        GameObject newItem = Instantiate(InventoryItemPrefab);
        newItem.transform.SetParent(slot, false);
        newItem.transform.localScale = Vector3.one;
        newItem.GetComponentInChildren<Image>().sprite = itemStack.item.Icon;
        InventoryItemImage itemImage = newItem.GetComponent<InventoryItemImage>();
        itemImage.currentItem = itemStack;
        itemImage.itemAmountText.GetComponent<TextMeshProUGUI>().text = itemStack.amount.ToString();
        itemImage.UiArea = area;
        invSlot.currentItem = itemStack;
        return newItem;
    }

    public void RemoveItem(InventoryItemImage itemImage)
    {
        //Remove item from game controller
        Destroy(itemImage.gameObject);
    }

    public GameObject LeftWheel
    {
        get { return _leftWheel; }
    }

    public GameObject RightWheel
    {
        get { return _rightWheel; }
    }

    public Player Player
    {
        get { return _player; }
    }
}
