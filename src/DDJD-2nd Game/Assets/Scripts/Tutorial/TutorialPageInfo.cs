using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TutorialPageInfo
{
    [SerializeField]
    private string _title;

    public string Title
    {
        get { return _title; }
    }

    [Multiline]
    [SerializeField]
    private string _text;

    public string Text
    {
        get { return _text; }
    }
    [SerializeField]
    private GameObject _gif;
    public GameObject Gif
    {
        get { return _gif; }
    }
}
