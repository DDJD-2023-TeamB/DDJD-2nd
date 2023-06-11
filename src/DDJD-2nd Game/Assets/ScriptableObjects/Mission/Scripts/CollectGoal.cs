using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CollectGoal : GoalObject
{
    [SerializeField]
    private CollectibleObject _collectibleToCollect;

    public CollectibleObject CollectibleToCollect
    {
        get { return _collectibleToCollect; }
    }

    [SerializeField]
    private int _quantity;

    public int Quantity
    {
        get { return _quantity; }
        set { _quantity = value; }
    }
}
