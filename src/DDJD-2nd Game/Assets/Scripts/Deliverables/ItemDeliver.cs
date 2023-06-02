using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDeliver : MonoBehaviour
{
    [SerializeField] private Text itemText;
    [SerializeField] private Text scoreText;

    private int items = 0;

    private void Start() {
        itemText.text = $"x{items}";
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Collectible")) {
            Destroy(collision.gameObject);
            items++;
            itemText.text = $"x{items}";
        }
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt("Items", items);
    }
}
