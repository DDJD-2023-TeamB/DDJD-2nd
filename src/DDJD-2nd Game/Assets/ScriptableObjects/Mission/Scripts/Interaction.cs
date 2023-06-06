using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName="Mission", menuName = "Scriptable Objects/Mission System/Interaction")]
public class Interaction : ScriptableObject
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
}