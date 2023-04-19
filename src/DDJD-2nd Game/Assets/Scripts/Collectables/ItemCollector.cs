using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemCollector : MonoBehaviour
{
    public Collectible collectible;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectibleManager collectibleManager = FindObjectOfType<CollectibleManager>();
            if (collectibleManager != null)
            {
                collectibleManager.Collect(collectible);
                Destroy(gameObject);
            }
        }
    }
}
