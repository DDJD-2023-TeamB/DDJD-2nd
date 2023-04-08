using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HandRune", menuName = "Scriptable Objects/Runes/HandRune", order = 1)]
public class HandRune : ScriptableObject
{
    [SerializeField]
    private GameObject _runePrefab;
}
