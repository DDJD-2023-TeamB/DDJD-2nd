using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Reward
{
    [SerializeField]
    private int _gold;
    [SerializeField]
    private List<CollectibleObject> _items = new List<CollectibleObject>();
}