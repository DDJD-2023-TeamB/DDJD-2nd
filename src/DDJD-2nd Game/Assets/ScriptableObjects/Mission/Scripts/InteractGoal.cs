using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InteractGoal : GoalObject
{
    [SerializeField]
    private Interaction _interaction;
    public Interaction Interaction
    {
        get { return _interaction; }
    }
}
