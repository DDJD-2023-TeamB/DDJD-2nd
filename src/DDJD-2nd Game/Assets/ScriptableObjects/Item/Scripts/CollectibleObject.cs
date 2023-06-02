using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectibleObject : ItemObject
{
    [SerializeField]
    protected GameObject _prefab;

    public abstract void Use();
}
