using UnityEngine;
using UnityEngine.UI;

public class CollectibleManager : MonoBehaviour
{
    public Text scoreText;
    public Collectible[] collectibles;

    private int score = 0;

    private void Start()
    {
        UpdateScore();
    }

    public void Collect(Collectible collectible)
    {
        score += collectible.score;
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = score.ToString();
    }
}