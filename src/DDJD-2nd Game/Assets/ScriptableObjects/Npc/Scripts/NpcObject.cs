using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC", menuName = "Scriptable Objects/NPC/NPC")]
public class NpcObject : ScriptableObject
{
    //TODO: Array (maybe queue?) de missions
    [SerializeField]
    private Mission2 _mission;

    public Mission2 Mission
    {
        get { return _mission; }
    }

    [SerializeField]
    private string _name;

    public string Name
    {
        get { return _name; }
    }

    [SerializeField]
    private DialogueInfo _defaultDialogueInfo;
    public DialogueInfo DefaultDialogueInfo
    {
        get { return _defaultDialogueInfo; }
    }

    //[SerializeField]
    //TODO: Location
}
