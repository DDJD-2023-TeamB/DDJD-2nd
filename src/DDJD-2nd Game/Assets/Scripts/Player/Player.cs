using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : StateContext, Damageable
{
    [SerializeField]
    private ItemsInventoryObject _inventory;
    public ItemsInventoryObject Inventory
    {
        get { return _inventory; }
        set { _inventory = value;}
    }

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

    [Header("Movement")]
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

    [SerializeField]
    private float _maxAirSpeed = 4f;
    public float MaxAirSpeed
    {
        get { return _maxAirSpeed; }
    }

    [SerializeField]
    private float _airAcceleration = 0.8f;
    public float AirAcceleration
    {
        get { return _airAcceleration; }
    }

    [SerializeField]
    private int _accelerationMultiplier = 1000;
    public int AccelerationMultiplier
    {
        get { return _accelerationMultiplier; }
    }
    private AimComponent _aimComponent;
    public AimComponent AimComponent
    {
        get { return _aimComponent; }
    }

    [SerializeField]
    [Tooltip("The material used when the player lands")]
    private PhysicMaterial _frictionlessMaterial;

    private PhysicMaterial _defaultMaterial;

    private Collider _collider;

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

    private PlayerDashable _dashable;
    public PlayerDashable Dashable
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

    public PlayerStatus Status
    {
        get { return _status; }
    }

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
    private CharacterStatus _characterStatus;
    public CharacterStatus CharacterStatus
    {
        get { return _characterStatus; }
    }
    private UIController _uiController;
    [SerializeField]
    private Dialogue _dialogue; //TODO:: Get from UI after UI PR merges
    private ElementController _elementController;
    private CharacterMovement _characterMovement;

    void Awake()
    {
        _inputReceiver = GetComponent<PlayerInputReceiver>();
        _rigidbody = GetComponent<Rigidbody>();
        _factory = new PlayerStateFactory(this);
        _animator = GetComponent<Animator>();
        _aimComponent = GetComponent<PlayerAimComponent>();
        _shooter = GetComponent<Shooter>();
        _objectSpawner = GetComponent<ObjectSpawner>();
        _playerSkills.Player = this;
        _meleeCombat = GetComponent<MeleeCombat>();
        _dashable = GetComponent<PlayerDashable>();
        _cameraController = GetComponent<CameraController>();
        _status = GetComponent<PlayerStatus>();
        _soundEmitter = GetComponent<SoundEmitter>();
        _characterStatus = GetComponent<CharacterStatus>();
        _uiController = GetComponent<UIController>();
        _elementController = GetComponent<ElementController>();
        _characterMovement = GetComponent<CharacterMovement>();
        _collider = GetComponent<Collider>();
        ChangeState(_factory.Playable());
    }

    void Start()
    {
        UpdateElement(null);
        _sfxJumpStateId = _soundEmitter.GetParameterId("jump", "Jump State");
        _sfxJumpIntensityId = _soundEmitter.GetParameterId("jump", "Jump Intensity");
        _inputReceiver.OnPrintState += () => _state?.PrintState();
        _defaultMaterial = _collider.material;
    }

    void Update()
    {
        _state.Update();
    }

    public void UpdateElement(Element element)
    {
        if (element != null)
        {
            _playerSkills.CurrentElement = element;
        }
        _airMovement = _playerSkills.CurrentElement?.AirMovementSkill?.Initialize(gameObject);
        _uiController.UpdateElements(
            _playerSkills.LeftSkill,
            _playerSkills.RightSkill,
            _playerSkills.CurrentElement
        );
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

    public PlayerStatus Status
    {
        get { return _status; }
    }

    public ElementController ElementController
    {
        get { return _elementController; }
    }

    public CharacterMovement CharacterMovement
    {
        get { return _characterMovement; }
    }

    public PhysicMaterial FrictionlessMaterial
    {
        get { return _frictionlessMaterial; }
    }

    public PhysicMaterial DefaultMaterial
    {
        get { return _defaultMaterial; }
    }

    public Collider Collider
    {
        get { return _collider; }
    }
}
