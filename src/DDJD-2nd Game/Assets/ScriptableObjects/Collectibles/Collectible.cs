using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Collectible", menuName = "Scriptable Objects/Collectibles", order = 3)]
public class Collectible : ScriptableObject
{
    public int id;
    public string name;
    public int value;
    public Sprite icon;

    public CollectibleType collectibleType;

    public enum CollectibleType{
        Potion,
        Book,
        QuestItem
    }

    //public abstract void Use();
}
