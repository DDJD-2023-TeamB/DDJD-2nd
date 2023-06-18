using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapAreaType
{
    Fire,
    Wind,
    Earth,
    Electricity
}

[CreateAssetMenu(fileName = "GameState", menuName = "Scriptable Objects/GameState")]
public class GameState : ScriptableObject
{
    [SerializeField]
    private List<Mission> _unblockedMissions = new List<Mission>();

    private List<Mission> _finishedMissions = new List<Mission>();

    public List<Mission> UnblockedMissions
    {
        get { return _unblockedMissions; }
        set { _unblockedMissions = value; }
    }

    public List<Mission> FinishedMissions
    {
        get { return _finishedMissions; }
    }

    [SerializeField]
    private List<MapAreaType> _unblockedAreas;
    public List<MapAreaType> UnblockedAreas
    {
        get { return _unblockedAreas; }
    }
}
