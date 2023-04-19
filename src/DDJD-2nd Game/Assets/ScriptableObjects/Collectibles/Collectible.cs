using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Collectible", menuName = "Scriptable Objects/Collectibles", order = 0)]

public class Collectible : ScriptableObject 
{
    [SerializeField] private CollectibleEffect effect;
    [SerializeField] private AudioSource audioSource;
    public string collectibleName;
    public int score;
    public Sprite icon;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (audioSource != null) {
                AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
            }
            effect.Apply(other.gameObject);
            Destroy(gameObject);
        }
    }
}
