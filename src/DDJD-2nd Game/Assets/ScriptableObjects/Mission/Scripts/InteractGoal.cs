using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InteractGoal : GoalObject
{
    [SerializeField]
    private NpcObject _npcToInteract;

    public NpcObject NpcToInteract
    {
        get { return _npcToInteract; }
    }
}
