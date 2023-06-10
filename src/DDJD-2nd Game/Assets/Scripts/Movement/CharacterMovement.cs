using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField]
    private float _acceleration = 0.0f;

    [SerializeField]
    private float _maxRunSpeed = 15.0f;

    [SerializeField]
    private float _maxSpeed = 7.5f;

    private bool _isRunning = false;

    [SerializeField]
    private float _maxAnimationSpeed = 1.75f;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public float GetCurrentMaxSpeed()
    {
        return _isRunning ? _maxRunSpeed : _maxSpeed;
    }

    public void Update() { }

    public bool IsRunning
    {
        get { return _isRunning; }
        set { _isRunning = value; }
    }
    public float Acceleration
    {
        get { return _acceleration; }
    }

    public float MaxRunSpeed
    {
        get { return _maxRunSpeed; }
    }

    public float MaxSpeed
    {
        get { return _maxSpeed; }
    }

    public float MaxAnimationSpeed
    {
        get { return _maxAnimationSpeed; }
    }
}
