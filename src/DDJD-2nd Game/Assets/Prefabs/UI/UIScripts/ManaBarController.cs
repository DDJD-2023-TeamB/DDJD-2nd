using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBarController : MonoBehaviour
{
    public Image outlineImage;
    public Image fillImage;

    private Slider _slider;

    protected void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    public void ChangeManaBar(ElementUI elementUI)
    {
        outlineImage.sprite = elementUI.Outline;
        fillImage.sprite = elementUI.Fill;
    }

    public float Value
    {
        get => _slider.value;
        set => _slider.value = value;
    }
}
