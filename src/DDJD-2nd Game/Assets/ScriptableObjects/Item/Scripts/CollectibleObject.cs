using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectibleObject : ItemObject
{
    [SerializeField]
    protected GameObject _prefab;

    [SerializeField]
    protected bool _hasToPay;

    //Make it conditional
    [SerializeField]
    protected int _cost;

    public int Cost
    {
        get => _cost;
    }

    public bool HasToPay
    {
        get => _hasToPay;
    }

    public abstract void Use(InventoryUI inventoryUI);
}
