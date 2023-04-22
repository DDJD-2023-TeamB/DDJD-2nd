using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DashStats
{
    [SerializeField]
    private float _force;
    public float Force
    {
        get => _force;
        set => _force = value;
    }

    [SerializeField]
    private float _duration;
    public float Duration
    {
        get => _duration;
        set => _duration = value;
    }
}
