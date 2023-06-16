using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectibleObject : ItemObject
{
    [SerializeField]
    protected GameObject _prefab;

    [SerializeField]
    protected bool _purchasable;

    //Make it conditional
    [SerializeField]
    protected int _cost;

    public int Cost
    {
        get => _cost;
    }

    public bool Purchasable
    {
        get => _purchasable;
    }

    public abstract void Use(InventoryUI inventoryUI);
}
