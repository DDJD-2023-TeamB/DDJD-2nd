using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveElementButton : MonoBehaviour
{
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private Sprite inactiveSprite;
    Image buttonImage;

    private void Start()
    {
        buttonImage = this.GetComponent<Image>();
    }
    public void setActive(bool active)
    {
        if (active)
        {
            buttonImage.sprite = activeSprite;
        }
        else
        {
            buttonImage.sprite = inactiveSprite;
        }
    }
}
