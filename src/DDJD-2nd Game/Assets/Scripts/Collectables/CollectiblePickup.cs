using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblePickup : Interactable
{
    public Collectible Collectible;

    void Pickup()
    {
        InventoryManager.Instance.Add(Collectible);
        Destroy(gameObject);
    }

    public override void Interact()
    {
        Pickup();
    }
}
