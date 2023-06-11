using JetBrains.Annotations;
using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
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

    private Tutorial _tutorial;
    public Tutorial Tutorial {
        get { return _tutorial; }
    }

    void Start()
    {
        ColorUtility.TryParseHtmlString("#FED600", out highlightColor);
        ShowUI();
    }


    private void ShowUI()
    {
        Debug.Log(_tutorial.TutorialPageInfo);
        _title.GetComponent<TextMeshProUGUI>().text = _tutorial.TutorialPageInfo[0].Title;
        GameObject tutorialInstance = Instantiate(_prefab, _content.transform);
        tutorialInstance.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = _tutorial.TutorialPageInfo[0].Text;
        tutorialInstance.transform.Find("Gif").GetComponent<Image>().sprite = _tutorial.TutorialPageInfo[0].Gif.GetComponent<Image>().sprite;
        tutorialInstance.transform.Find("Gif").GetComponent<Animator>().runtimeAnimatorController = _tutorial.TutorialPageInfo[0].Gif.GetComponent<Animator>().runtimeAnimatorController;
    }

}
