using JetBrains.Annotations;
using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _description;
    [SerializeField]
    private GameObject _image;
    [SerializeField]
    private GameObject _title;
    Color highlightColor;
    void Start()
    {
        ColorUtility.TryParseHtmlString("#FED600", out highlightColor);
    }


    public void ShowUI(Tutorial tutorial)
    {
        // Replace title
        _title.GetComponent<TextMeshProUGUI>().text = tutorial.TutorialInfo[tutorial.CurrentPage].Title;
       
        // Replace description
        _description.GetComponent<TextMeshProUGUI>().text = tutorial.TutorialInfo[tutorial.CurrentPage].Text;

        // Replace Image
        _image.GetComponent<Image>().sprite = Sprite.Create(
            tutorial.TutorialInfo[tutorial.CurrentPage].Image,
            new Rect(0, 0, tutorial.TutorialInfo[tutorial.CurrentPage].Image.width, tutorial.TutorialInfo[tutorial.CurrentPage].Image.height),
            Vector2.one * 0.5f
        );
    }

}
