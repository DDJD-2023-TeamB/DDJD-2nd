using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Collectible", menuName = "Scriptable Objects/Collectibles", order = 3)]

public class Collectible : ScriptableObject 
{
    public int score;
}
