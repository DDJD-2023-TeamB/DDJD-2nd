using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectibleInteractable : Interactable
{
    public override void Interact()
    {
        Debug.Log("Pickup");
    }
}
