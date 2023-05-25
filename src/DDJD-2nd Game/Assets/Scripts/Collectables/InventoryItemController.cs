using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InventoryItemController : MonoBehaviour 
{
    public Collectible collectible;

    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(collectible);
        Destroy(gameObject);
    }

    public void AddItem(Collectible newCollectible)
    {
        collectible = newCollectible;
    } 

    public void UseItem()
    {
        switch(collectible.collectibleType)
        {
            case Collectible.CollectibleType.Book:
                Debug.Log("Using Book");
                break;
            case Collectible.CollectibleType.Potion:
                Debug.Log("Using Potion");
                break;
            case Collectible.CollectibleType.QuestItem:
                // TODO VER ISTO
                Debug.Log("Using QuestItem");
                break;
        }

        RemoveItem();
    }
}
