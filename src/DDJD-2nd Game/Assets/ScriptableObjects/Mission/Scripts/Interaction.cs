using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

[System.Serializable]
public class Interaction
{
    [SerializeField]
    private DialogueInfo _dialogueInfo;

    public DialogueInfo DialogueInfo
    {
        get { return _dialogueInfo; }
    }

    [SerializeField]
    private NpcObject _npc;

    public NpcObject Npc
    {
        get { return _npc; }
    }

    [SerializeField]
    private UnityEvent _onEndInteraction = new UnityEvent();
    public UnityEvent OnEndInteraction
    {
        get { return _onEndInteraction; }
    }

}
