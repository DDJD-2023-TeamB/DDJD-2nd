using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="GameState", menuName = "Scriptable Objects/GameState")]
public class GameState : ScriptableObject
{
    //TODO HOJE
    [SerializeField]
    private List<Mission2> _unblockedMissions = new List<Mission2>();
    
    //mapAreas
}