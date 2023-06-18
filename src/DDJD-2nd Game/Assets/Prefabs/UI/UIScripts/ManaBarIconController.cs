using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBarIconController : MonoBehaviour
{

    private Image imageComponent;
    private void Start()
    {
        imageComponent = GetComponent<Image>();
    }
    public void changeSpellSprite(Sprite sprite)
    {

        if (imageComponent == null)
        {
            imageComponent = GetComponent<Image>();
        }

        if (sprite == null)
        {
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(true);

        imageComponent.sprite = sprite;

    }
}
