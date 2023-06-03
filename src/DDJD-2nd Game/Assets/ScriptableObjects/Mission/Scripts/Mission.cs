using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MissionState
{
    Blocked,
    Available,
    Ongoing,
    Finished
}
public enum MissionType{
    Story, 
    Secondary
}
[CreateAssetMenu(fileName="Mission", menuName = "Scriptable Objects/Mission System/Mission")]
public class Mission2 : ScriptableObject
{
    private MissionState _status = MissionState.Blocked;
    [SerializeField]
    private List<Goal> _goals = new List<Goal>();
    //[SerializeField]
    //private Interaction _interactionBegin = new Interaction();
    //[SerializeField]
    //private Interaction _interactionEnd = new Interaction();
    //private Reward _reward;
    [SerializeField]
    private List<Mission2> _unblockedMissions = new List<Mission2>();
    [SerializeField]
    private MissionType _type;
    //mapAreas
    //onFinished -> callback
}