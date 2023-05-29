using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectibleInteractable : Interactable
{
    public Item _item;
    public override void Interact()
    {
        // add item to inventory
        Debug.Log("Interacting with " + _item.name);
        GameObject.FindWithTag("Player").GetComponent<InventoryManager>().AddItem(_item);
        Destroy(gameObject);
    }
}
