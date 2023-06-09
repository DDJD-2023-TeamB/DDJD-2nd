using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GoalObject : ScriptableObject
{
    public bool _completed = false;
    [SerializeField]
    private string _description;

    //Hint
    [SerializeField]
    private Location _location;
    public Location Location
    {
        get { return _location; }
    }

}