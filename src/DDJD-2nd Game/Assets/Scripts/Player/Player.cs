using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : StateContext, Damageable
{
    public ItemsInventoryObject inventory;

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

    private ObjectSpawner _objectSpawner;
    public ObjectSpawner ObjectSpawner
    {
        get { return _objectSpawner; }
    }

    private Dashable _dashComponent;
    public Dashable DashComponent
    {
        get { return _dashComponent; }
    }

    [Header("Movement")]
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

    public MeleeCombat _meleeCombat;
    public MeleeCombat MeleeCombat
    {
        get { return _meleeCombat; }
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

    private Dashable _dashable;
    public Dashable Dashable
    {
        get { return _dashable; }
    }

    public Interactable _interactedObject;
    private AirMovementComponent _airMovement;
    public AirMovementComponent AirMovement
    {
        get { return _airMovement; }
    }

    private CameraController _cameraController;

    public CameraController CameraController
    {
        get { return _cameraController; }
    }

    private PlayerStatus _status;

    private SoundEmitter _soundEmitter;

    private FMOD.Studio.PARAMETER_ID _sfxJumpStateId;

    public FMOD.Studio.PARAMETER_ID SfxJumpStateId
    {
        get { return _sfxJumpStateId; }
    }

    public SoundEmitter SoundEmitter
    {
        get { return _soundEmitter; }
    }

    private FMOD.Studio.PARAMETER_ID _sfxJumpIntensityId;
    public FMOD.Studio.PARAMETER_ID SfxJumpIntensityId
    {
        get { return _sfxJumpIntensityId; }
    }
    private UIController _uiController;
    [SerializeField]
    private Dialogue _dialogue; //TODO:: Get from UI after UI PR merges

    void Awake()
    {
        _inputReceiver = GetComponent<PlayerInputReceiver>();
        _rigidbody = GetComponent<Rigidbody>();
        _factory = new PlayerStateFactory(this);
        _animator = GetComponent<Animator>();
        _aimComponent = GetComponent<PlayerAimComponent>();
        _shooter = GetComponent<Shooter>();
        _objectSpawner = GetComponent<ObjectSpawner>();
        _dashComponent = GetComponent<Dashable>();
        _playerSkills.Player = this;
        _meleeCombat = GetComponent<MeleeCombat>();
        _dashable = GetComponent<Dashable>();
        _cameraController = GetComponent<CameraController>();
        _status = GetComponent<PlayerStatus>();
        _soundEmitter = GetComponent<SoundEmitter>();
        _uiController = GetComponent<UIController>();
        ChangeState(_factory.Playable());
    }

    void Start()
    {
        UpdateElement();
        _sfxJumpStateId = _soundEmitter.GetParameterId("jump", "Jump State");
        _sfxJumpIntensityId = _soundEmitter.GetParameterId("jump", "Jump Intensity");
    }

    void Update()
    {
        _state.Update();
    }

    void UpdateElement()
    {
        _airMovement = _playerSkills.CurrentElement?.AirMovementSkill?.Initialize(gameObject);
    }

    public void TakeDamage(
        GameObject damager,
        int damage,
        float force,
        Vector3 hitPoint,
        Vector3 hitDirection,
        Element element
    )
    {
        _status.TakeDamage(damager, damage, hitPoint, hitDirection);
    }

    public bool IsTriggerDamage()
    {
        return false;
    }

    public GameObject GetDamageableObject()
    {
        return this.gameObject;
    }

    public UIController UIController
    {
        get { return _uiController; }
    }
    public Dialogue Dialogue
    {
        get { return _dialogue; }
    }

    public Interactable InteractedObject
    {
        get { return _interactedObject; }
        set { _interactedObject = value; }
    }
}
