using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public abstract class GoalObject
{
    public bool _completed = false;

    [SerializeField]
    private UnityEvent _onGoalCompleted = new UnityEvent();

    [SerializeField]
    private string _description;

    //Hint
    [SerializeField]
    private Location _location;
    public Location Location
    {
        get { return _location; }
    }

    public UnityEvent OnGoalCompleted
    {
        get { return _onGoalCompleted; }
    }
}
