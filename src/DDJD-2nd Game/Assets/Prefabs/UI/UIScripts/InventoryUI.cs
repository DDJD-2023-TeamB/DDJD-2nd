using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InventoryUI : MonoBehaviour
{

    public GameObject InventoryItemPrefab;

    public GameObject itemsPanel;
    public GameObject spellsPanel;

    [SerializeField]
    private GameObject _leftWheel;

    [SerializeField]
    private GameObject _rightWheel;

    public Action OnItemRemoved;
    public Action<ItemStack, int> OnItemDrop;
    public Action<ItemStack, int> OnItemSkillDrop;
    public Action<ItemStack, int> OnItemSkillLeftDrop;
    public Action<ItemStack, int> OnItemSkillRightDrop;

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
                Debug.Log("I" + i);
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
            if (itemsPanel.transform.GetChild(i).childCount == 0)
            {
                return itemsPanel.transform.GetChild(i);
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

    public bool AddItem(ItemStack item)
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
        GameObject newItem = CreateItemSlot(item, availableSlot);
        return true;
    }

    public void RemoveAllItems()
    {
        for (int i = 0; i < itemsPanel.transform.childCount; i++)
        {
            if (itemsPanel.transform.GetChild(i).childCount != 0)
            {   
                GameObject itemObject = itemsPanel.transform.GetChild(i).gameObject;
                Destroy(itemObject);           
            }
        }
    }

    public bool RemoveItem(ItemStack item)
    {
        if (item.item is ItemSkill)
        {
            for (int i = 0; i < spellsPanel.transform.childCount; i++)
            {
                if (spellsPanel.transform.GetChild(i).childCount != 0)
                {
                    if (
                        spellsPanel.transform
                            .GetChild(i)
                            .GetChild(0)
                            .GetComponent<InventoryItemImage>()
                            .currentItem == item
                    )
                    {
                        Destroy(spellsPanel.transform.GetChild(i).GetChild(0));
                        return true;
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
                    if (
                        itemsPanel.transform
                            .GetChild(i)
                            .GetChild(0)
                            .GetComponent<InventoryItemImage>()
                            .currentItem == item
                    )
                    {
                        Destroy(itemsPanel.transform.GetChild(i).GetChild(0));
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public void SetLeftWheelSkills(List<ItemSkill> skills)
    {
        for (int i = 0; i < skills.Count; i++)
        {
            if (skills[i] == null)
            {
                continue;
            }
            Transform slot = GetLeftSkillSlot(i);
            //Remove children
            ItemStack item = new ItemStack(skills[i],1, null);
            GameObject newItem = CreateItemSlot(item, slot);
        }
    }

    public void SetRightWheelSkills(List<ItemSkill> skills)
    {
        for (int i = 0; i < skills.Count; i++)
        {
            if (skills[i] == null)
            {
                continue;
            }
            Transform slot = GetRightSkillSlot(i);
            ItemStack item = new ItemStack(skills[i],1, null);
            GameObject newItem = CreateItemSlot(item, slot);
        }
    }

    private GameObject CreateItemSlot(ItemStack ItemStack, Transform slot)
    {
        InventorySlot invSlot = slot.GetComponent<InventorySlot>();
        if (invSlot.currentItem != null)
        {
            RemoveItem(invSlot.currentItem);
        }
        GameObject newItem = Instantiate(InventoryItemPrefab);
        newItem.transform.SetParent(slot, false);
        newItem.transform.localScale = Vector3.one;
        newItem.GetComponentInChildren<Image>().sprite = ItemStack.item.Icon;
        newItem.GetComponent<InventoryItemImage>().currentItem = ItemStack;
        newItem
            .GetComponent<InventoryItemImage>()
            .itemAmountText.GetComponent<TextMeshProUGUI>()
            .text = ItemStack.amount.ToString();
        invSlot.currentItem = ItemStack;
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
}
