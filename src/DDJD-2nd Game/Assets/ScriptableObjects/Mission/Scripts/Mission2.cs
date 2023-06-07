using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MissionState
{
    Blocked,
    Available,
    Ongoing,
    Completed
}
public enum MissionType{
    Story, 
    Secondary
}
[CreateAssetMenu(fileName="Mission", menuName = "Scriptable Objects/Mission System/Mission")]
public class Mission2 : ScriptableObject
{
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
    
    [SerializeField]
    private Interaction _interactionBegin;
    public Interaction InteractionBegin
    {
        get => _interactionBegin;
    }

    [SerializeField]
    private Interaction _interactionEnd;
    public Interaction InteractionEnd
    {
        get => _interactionEnd;
    }
    
   
    [SerializeField]
    private Reward _reward;

    public Reward Reward
    {
        get { return _reward; }
    }

    [SerializeField]
    private List<Mission> _unblockedMissions = new List<Mission>();

    public List<Mission> UnblockedMissions
    {
        get { return _unblockedMissions; }
    }

    //TODO AMANHA
    //mapAreas


    //onFinished -> callback
}