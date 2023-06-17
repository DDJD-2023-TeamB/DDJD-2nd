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

    void Start()
    {
        ColorUtility.TryParseHtmlString("#FED600", out highlightColor);
    }


    public void ShowUI(Tutorial tutorial)
    {
        Debug.Log(tutorial.TutorialInfo[tutorial.CurrentPage]);
        _title.GetComponent<TextMeshProUGUI>().text = tutorial.TutorialInfo[tutorial.CurrentPage].Title;
        GameObject tutorialInstance = Instantiate(_prefab, _content.transform);
        tutorialInstance.transform.Find("Description").GetComponent<TextMeshProUGUI>().text =tutorial.TutorialInfo[tutorial.CurrentPage].Text;
        tutorialInstance.transform.Find("Gif").GetComponent<Image>().sprite = tutorial.TutorialInfo[tutorial.CurrentPage].Gif.GetComponent<Image>().sprite;
        tutorialInstance.transform.Find("Gif").GetComponent<Animator>().runtimeAnimatorController = tutorial.TutorialInfo[tutorial.CurrentPage].Gif.GetComponent<Animator>().runtimeAnimatorController;
    }

}
