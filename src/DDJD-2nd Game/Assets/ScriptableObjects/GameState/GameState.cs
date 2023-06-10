using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapAreaType
{
    Fire,
    Wind,
    Ground,
    Electricity
}

[CreateAssetMenu(fileName = "GameState", menuName = "Scriptable Objects/GameState")]
public class GameState : ScriptableObject
{
    [SerializeField]
    private List<Mission2> _unblockedMissions = new List<Mission2>();

    public List<Mission2> UnblockedMissions
    {
        get { return _unblockedMissions; }
        set { _unblockedMissions = value; }
    }

    [SerializeField]
    private List<MapAreaType> _unblockedAreas;
    public List<MapAreaType> UnblockedAreas
    {
        get { return _unblockedAreas; }
    }
}
