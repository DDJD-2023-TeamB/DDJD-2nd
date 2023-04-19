using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemCollector : MonoBehaviour
{
    public Collectible collectible;
    public Text scoreText;

    private int score = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
            if (inventoryManager != null)
            {
                inventoryManager.Collect(collectible);
                Destroy(gameObject);
            }
        }
    }

}
