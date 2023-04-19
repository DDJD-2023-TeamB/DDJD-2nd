using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] public Collectible[] collectibles;

    private int score = 0;

    public void Collect(Collectible collectible)
    {
        Array.Resize(ref collectibles, collectibles.Length + 1);
        collectibles[collectibles.Length - 1] = collectible;
        score += collectible.score;
        Debug.Log("Score: " + collectibles);
    }
}