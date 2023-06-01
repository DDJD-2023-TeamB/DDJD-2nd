using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Book", menuName = "Scriptable Objects/Items/Book")]
public class Book : CollectibleObject
{
    public void Awake()
    {
        _type = ItemType.Book;
    }

    public override void Use(){
        Debug.Log("Using book");
    }
}