using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "CollectibleGoalData",
    menuName = "ScriptableObjects/Mission/Goals/CollectibleGoalData",
    order = 1
)]
public class CollectibleGoalData : ScriptableObject
{
    [SerializeField]
    private List<GameObject> _prefabs;

    public List<GameObject> Prefabs
    {
        get { return _prefabs; }
        set { _prefabs = value; }
    }
}
