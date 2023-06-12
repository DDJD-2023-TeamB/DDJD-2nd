using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Reward
{
    [SerializeField]
    private int _gold;

    public int Gold
    {
        get { return _gold; }
    }

    [SerializeField]
    private List<ItemStack> _items = new List<ItemStack>();

    public List<ItemStack> Items
    {
        get { return _items; }
    }
}