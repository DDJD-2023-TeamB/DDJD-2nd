using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectibleInteractable : Interactable
{
    public CollectibleObject _item;

    public override void Interact()
    {
        GameObject
            .FindGameObjectWithTag("Player")
            .GetComponent<Player>()
            .Inventory.AddItem(_item, 1);
        Destroy(gameObject);
    }
}
