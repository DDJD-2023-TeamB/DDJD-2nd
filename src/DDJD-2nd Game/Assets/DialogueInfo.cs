using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueInfo
{
    [SerializeField]
    private string _noun;

    [TextArea(3, 10)]
    [SerializeField]
    private string[] _sentences;

    public string[] Sentences
    {
        get { return _sentences; }
    }

    public string Noun
    {
        get { return _noun; }
    }
}
