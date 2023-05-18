using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{

    public GameObject InventoryItemPrefab;

    public GameObject itemsPanel;
    public GameObject spellsPanel;

    public Transform FindAvailableItemSlot()
    {
        for (int i = 0; i < itemsPanel.transform.childCount; i++)
        {
            if(itemsPanel.transform.GetChild(i).childCount == 0)
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




    public bool AddItem(ItemStack item)
    {
        Transform availableSlot;
        if (item.type.isSpell)
        {
            availableSlot = FindAvailableSpellSlot();
        }
        else 
        {
            availableSlot= FindAvailableItemSlot();
        }
        if(availableSlot == null)
        {
            return false;
        }
        GameObject newItem = Instantiate(InventoryItemPrefab);
        newItem.transform.SetParent(availableSlot,false);
        newItem.transform.localScale = Vector3.one;
        newItem.GetComponentInChildren<Image>().sprite = item.type.itemSprite;
        newItem.GetComponent<InventoryItemImage>().currentItem = item;
        newItem.GetComponent<InventoryItemImage>().itemAmountText.GetComponent<TextMeshProUGUI>().text = item.amount.ToString();
        availableSlot.GetComponent<InventorySlot>().currentItem = item;
        return true;
    }

    public bool RemoveItem(ItemStack item)
    {
        if (item.type.isSpell)
        {
            for (int i = 0; i < spellsPanel.transform.childCount; i++)
            {
                if (spellsPanel.transform.GetChild(i).childCount != 0)
                {
                    if (spellsPanel.transform.GetChild(i).GetChild(0).GetComponent<InventoryItemImage>().currentItem == item)
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
                    if (itemsPanel.transform.GetChild(i).GetChild(0).GetComponent<InventoryItemImage>().currentItem == item)
                    {
                        Destroy(itemsPanel.transform.GetChild(i).GetChild(0));
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public void RemoveItem(InventoryItemImage itemImage)
    {
        //Remove item from game controller
        Destroy(itemImage.gameObject);
    }


}


