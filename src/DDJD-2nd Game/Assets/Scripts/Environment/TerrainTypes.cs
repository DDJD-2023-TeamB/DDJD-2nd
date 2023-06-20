using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTypes : MonoBehaviour
{
    [SerializeField]
    private List<string> _floorTypes;

    public string GetFloorType(int index)
    {
        return _floorTypes[index];
    }

    public int GetCount()
    {
        return _floorTypes.Count;
    }
}
