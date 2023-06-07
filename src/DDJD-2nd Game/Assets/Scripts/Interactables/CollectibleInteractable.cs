using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectibleInteractable : Interactable
{
    public CollectibleObject _item;
    public override void Interact()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().inventory.AddItem(_item, 1);
        Destroy(gameObject);
        _missionController.CheckIfItemCollectedIsMyGoal(_item);
    }
}
