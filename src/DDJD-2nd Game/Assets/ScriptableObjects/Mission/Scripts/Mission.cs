using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum MissionState
{
    Blocked,
    Available,
    Ongoing,
    Completed
}

public enum MissionType
{
    Story,
    Secondary
}

[CreateAssetMenu(fileName = "Mission", menuName = "Scriptable Objects/Mission System/Mission")]
public class Mission : ScriptableObject
{
    [SerializeField]
    private string _title;

    public string Title
    {
        get { return _title; }
    }

    [SerializeField]
    private string _description;

    public string Description
    {
        get { return _description; }
    }

    [SerializeField]
    private MissionState _status = MissionState.Blocked;

    public MissionState Status
    {
        get { return _status; }
        set { _status = value; }
    }

    [SerializeField]
    private MissionType _type;

    public MissionType Type
    {
        get { return _type; }
    }

    [SerializeField]
    private List<GoalObject> _goals = new List<GoalObject>();

    public List<GoalObject> Goals
    {
        get { return _goals; }
    }

    private GoalObject _currentGoal;
    public GoalObject CurrentGoal
    {
        get { return _currentGoal; }
        set { _currentGoal = value; }
    }

    [SerializeField]
    private Interaction _interactionBegin;
    public Interaction InteractionBegin
    {
        get => _interactionBegin;
    }

    [SerializeField]
    private Reward _reward;

    public Reward Reward
    {
        get { return _reward; }
    }

    [SerializeField]
    private List<Mission> _followingMissions = new List<Mission>();

    public List<Mission> FollowingMissions
    {
        get { return _followingMissions; }
    }

    [SerializeField]
    private MapAreaType _area;

    public MapAreaType Area
    {
        get { return _area; }
    }
    public void OnEnable() { }

    public void Unblock()
    {
        if (_status == MissionState.Completed)
            return;

        _status = MissionState.Available;
        if (_goals.Count > 0)
            _currentGoal = _goals[0];
    }

    public void CompleteCurrentGoal()
    {
        _currentGoal.Complete();
        int nextGoalIndex = _goals.IndexOf(_currentGoal) + 1;
        if (nextGoalIndex < _goals.Count)
        {
            _currentGoal = _goals[nextGoalIndex];
            _currentGoal.OnGoalStarted?.Invoke();
        }
        else
        {
            _currentGoal = null;
        }
    }

    public bool IsCompleted()
    {
        return _currentGoal == null;
    }
}
