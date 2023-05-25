using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Potion", menuName = "Items/Potion")]
public class Potion : Collectible
{
    public override void Use(){
        Debug.Log("Using potion");
    }
}