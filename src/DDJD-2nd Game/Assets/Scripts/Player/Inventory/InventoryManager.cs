using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InventoryManager : MonoBehaviour
{
    ItemStack[] itemList = new ItemStack[30];

    public bool AddItem(Item item)
    {
        Debug.Log("Adding item");
        ItemStack itemToAdd = new ItemStack(item, null);
        int index = Array.IndexOf(itemList, itemToAdd);
        if (index != -1)
        {
            itemList[index].amount += itemToAdd.amount;
            return true;
        }

        index = Array.IndexOf(itemList, null);
        if (index != -1)
        {
            itemList[index] = itemToAdd;
            return true;
        }

        return false;
    }

    
    public bool RemoveItem(Item item)
    {
        ItemStack itemToRemove = new ItemStack(item, null);
        int index = Array.IndexOf(itemList, itemToRemove);

        if (index != -1)
        {
            itemList[index] = null;
            return true;
        }
        return false;
    }
}
