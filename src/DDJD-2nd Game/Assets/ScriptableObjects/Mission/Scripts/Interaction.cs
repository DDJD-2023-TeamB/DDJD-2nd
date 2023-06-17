using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //Unity event 
}
