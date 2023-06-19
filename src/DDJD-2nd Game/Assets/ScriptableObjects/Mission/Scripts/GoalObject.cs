using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

[System.Serializable]
public abstract class GoalObject : ScriptableObject
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

    public void OnGUI()
    {
        Editor editor = Editor.CreateEditor(this);
        editor?.OnInspectorGUI();
    }

    public string Description
    {
        get { return _description; }
    }

    public bool Completed
    {
        get { return _completed; }
    }

    public void Complete()
    {
        _onGoalCompleted?.Invoke();
    }
}
