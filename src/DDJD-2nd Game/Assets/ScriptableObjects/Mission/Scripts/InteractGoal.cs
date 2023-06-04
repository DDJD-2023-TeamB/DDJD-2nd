using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Interact", menuName = "Scriptable Objects/Mission System/Goal/Interact")]
public class InteractGoal : GoalObject
{
    //add here a npc
    [SerializeField]
    public GameObject npcToInteract;
}