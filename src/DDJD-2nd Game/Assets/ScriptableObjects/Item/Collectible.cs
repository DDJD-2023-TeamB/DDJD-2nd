using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Collectible", menuName = "Scriptable Objects/Collectibles", order = 3)]
public abstract class Collectible : Item
{
    [SerializeField]
    protected GameObject _prefab;

    public abstract void Use();
}
