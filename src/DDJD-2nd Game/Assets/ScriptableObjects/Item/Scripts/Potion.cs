using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Potion", menuName = "Scriptable Objects/Items/Potion")]
public class Potion : CollectibleObject
{
    public int restoreHealthValue;

    public override void Use(){
        // TODO : update _health
        Debug.Log("Using potion");
    }
}

