using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectibleInteractable : Interactable
{
    public CollectibleObject _item;
    public override void Interact()
    {
        Debug.Log("Interacting with " + _item.Type);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().inventory.AddItem(_item, 1);
        Destroy(gameObject);
    }
}
