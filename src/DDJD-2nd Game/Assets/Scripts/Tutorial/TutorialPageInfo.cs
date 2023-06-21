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
    private Texture2D _image;
    public Texture2D Image
    {
        get { return _image; }
    }
}
