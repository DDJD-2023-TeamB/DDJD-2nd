using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private Text itemText;

    private int items = 0;
    private int score = 0;

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

    public void IncreaseScore(int points) {
        score += points;
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("Items", items);
    }
}
