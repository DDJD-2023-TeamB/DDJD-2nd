using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="QuestItem", menuName = "Items/QuestItem")]
public class QuestItem : CollectibleObject
{
    public void Awake()
    {
        _type = ItemType.QuestItem;
    }

    public override void Use(){
        Debug.Log("Using quest item");
    }
}