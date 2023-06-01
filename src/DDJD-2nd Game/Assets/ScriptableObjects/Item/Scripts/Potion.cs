using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Potion", menuName = "Scriptable Objects/Items/Potion")]
public class Potion : CollectibleObject
{
    public int restoreHealthValue;
    public void Awake()
    {
        _type = ItemType.Potion;
    }

    public override void Use(){
        Debug.Log("Using potion");
    }
}

