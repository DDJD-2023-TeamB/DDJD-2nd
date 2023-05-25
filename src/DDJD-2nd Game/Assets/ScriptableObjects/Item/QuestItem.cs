using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="QuestItem", menuName = "Items/QuestItem")]
public class QuestItem : Collectible
{
    public override void Use(){
        Debug.Log("Using quest item");
    }
}