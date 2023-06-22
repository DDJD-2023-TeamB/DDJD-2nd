using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC", menuName = "Scriptable Objects/NPC/NPC")]
public class NpcObject : ScriptableObject
{
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

    [SerializeField]
    private Location _location;
    public Location Location
    {
        get { return _location; }
    }
}
