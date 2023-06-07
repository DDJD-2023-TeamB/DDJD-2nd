using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "Interact",
    menuName = "Scriptable Objects/Mission System/Goal/Interact"
)]
public class InteractGoal : GoalObject
{
    [SerializeField]
    private NpcObject _npcToInteract;

    public NpcObject NpcToInteract
    {
        get { return _npcToInteract; }
    }
}
