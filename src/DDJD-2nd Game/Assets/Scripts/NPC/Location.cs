using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Location : MonoBehaviour
{
    [SerializeField]
    private MapAreaType _area;

    public MapAreaType Area
    {
        get { return _area; }
    }

    // TODO : confirmar
    [SerializeField]
    private Transform _position;

    public Transform Position
    {
        get { return _position; }
    }

    [SerializeField]
    private float _radius;

    public float Radius
    {
        get { return _radius; }
    }

}
