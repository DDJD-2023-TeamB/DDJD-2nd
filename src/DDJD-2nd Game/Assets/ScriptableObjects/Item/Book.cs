using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Book", menuName = "Items/Book")]
public class Book : Collectible
{
    public override void Use(){
        Debug.Log("Using book");
    }
}