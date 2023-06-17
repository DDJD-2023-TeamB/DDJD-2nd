using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public abstract class CollectibleObject : ItemObject
{
    [SerializeField]
    protected GameObject _prefab;

    [SerializeField]
    protected bool _purchasable;

    public bool Purchasable
    {
        get => _purchasable;
    }

    [ConditionalField(nameof(_purchasable), false, true)]
    [SerializeField]
    protected int _cost;

    public int Cost
    {
        get => _cost;
    }
 
    [ConditionalField(nameof(_purchasable), false, true)]
    [SerializeField]
    protected int _numLeftToBuy;

    public int NumLeftToBuy
    {
        get => _numLeftToBuy;
    }

    public void BuyItem()
    {
        _numLeftToBuy -= 1;
    }

    public abstract void Use(InventoryUI inventoryUI);
}
