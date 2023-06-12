using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestItem", menuName = "Scriptable Objects/Items/QuestItem")]
public class QuestItem : CollectibleObject
{
    public override void Use(InventoryUI inventoryUI)
    {
        Debug.Log("Nothing");
    }
}
