using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : StateContext
{
    private PlayerStateFactory _factory;
    public PlayerStateFactory Factory
    {
        get { return _factory; }
    }

    private PlayerInputReceiver _inputReceiver;

    public PlayerInputReceiver Input
    {
        get { return _inputReceiver; }
    }

    private Rigidbody _rigidbody;
    public Rigidbody Rigidbody
    {
        get { return _rigidbody; }
    }

    private Animator _animator;
    public Animator Animator
    {
        get { return _animator; }
    }

    private Shooter _shooter;
    public Shooter Shooter
    {
        get { return _shooter; }
    }

    [Header("Player Movement")]
    [SerializeField]
    private float _maxSpeed = 5f;
    public float MaxSpeed
    {
        get { return _maxSpeed; }
    }

    [SerializeField]
    private float _jumpForce = 20f;
    public float JumpForce
    {
        get { return _jumpForce; }
    }

    [SerializeField]
    private float _acceleration = 2.0f;
    public float Acceleration
    {
        get { return _acceleration; }
    }

    [Header("Camera movement")]
    [SerializeField]
    private Transform _cameraTarget;
    public Transform CameraTarget
    {
        get { return _cameraTarget; }
    }

    [SerializeField]
    private float _cameraRotationSpeed = 20.0f;
    public float CameraRotationSpeed
    {
        get { return _cameraRotationSpeed; }
    }

    [SerializeField]
    private float _minAngle = 30f;
    public float MinAngle
    {
        get { return _minAngle; }
    }

    [SerializeField]
    private float maxAngle = 330f;
    public float MaxAngle
    {
        get { return maxAngle; }
    }

    [SerializeField]
    private CinemachineVirtualCamera _aimCamera;
    public CinemachineVirtualCamera AimCamera
    {
        get { return _aimCamera; }
    }

    private AimComponent _aimComponent;
    public AimComponent AimComponent
    {
        get { return _aimComponent; }
    }

    [Header("Abilities")]
    [SerializeField]
    private PlayerSkills _playerSkills;
    public PlayerSkills PlayerSkills
    {
        get { return _playerSkills; }
    }

    [SerializeField]
    private GameObject _LeftHand;
    public GameObject LeftHand
    {
        get { return _LeftHand; }
    }

    [SerializeField]
    private GameObject _RightHand;
    public GameObject RightHand
    {
        get { return _RightHand; }
    }

    void Awake()
    {
        _inputReceiver = GetComponent<PlayerInputReceiver>();
        _rigidbody = GetComponent<Rigidbody>();
        _factory = new PlayerStateFactory(this);
        _animator = GetComponent<Animator>();
        _aimComponent = GetComponent<AimComponent>();
        _shooter = GetComponent<Shooter>();
        _playerSkills.Player = this;
        ChangeState(_factory.Playable());
    }

    void Update()
    {
        _state.Update();
    }
}
