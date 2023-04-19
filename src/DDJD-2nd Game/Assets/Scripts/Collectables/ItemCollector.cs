using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemCollector : MonoBehaviour
{
    [SerializeField] private Text itemText;
    [SerializeField] private Text scoreText;

    private int items = 0;
    private int score = 0;

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
