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
    private GameObject _prefab;
    public GameObject Prefab {
        get { return _prefab; }
    }
    [SerializeField]
    private GameObject _content;
    public GameObject Content {
        get { return _content; }
    }
    [SerializeField]
    private GameObject _title;
    public GameObject Title {
        get { return _title; }
    }
    Color highlightColor;
    private GameObject _tutorialInstance;

    void Start()
    {
        ColorUtility.TryParseHtmlString("#FED600", out highlightColor);
    }


    public void ShowUI(Tutorial tutorial)
    {
        if (_tutorialInstance == null) _tutorialInstance = Instantiate(_prefab, _content.transform);
        
        // Replace title
        _title.GetComponent<TextMeshProUGUI>().text = tutorial.TutorialInfo[tutorial.CurrentPage].Title;
       
        // Replace description
        _tutorialInstance.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = tutorial.TutorialInfo[tutorial.CurrentPage].Text;

        // Replace GIF
        GameObject gifGameObject = _tutorialInstance.transform.Find("Gif").gameObject;
        gifGameObject.GetComponent<Image>().sprite = tutorial.TutorialInfo[tutorial.CurrentPage].Gif.GetComponent<Image>().sprite;
        gifGameObject.GetComponent<Animator>().runtimeAnimatorController = tutorial.TutorialInfo[tutorial.CurrentPage].Gif.GetComponent<Animator>().runtimeAnimatorController;
    }

}
