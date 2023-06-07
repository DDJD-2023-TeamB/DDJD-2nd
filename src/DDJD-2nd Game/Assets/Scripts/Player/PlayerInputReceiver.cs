using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInputReceiver : MonoBehaviour
{
    private PlayerInput _playerInput;

    private Vector2 _moveInput;
    public Vector2 MoveInput
    {
        get { return _moveInput; }
    }

    private Vector2 _lookInput;
    public Vector2 LookInput
    {
        get { return _lookInput; }
    }

    private bool _isRunning;
    public bool IsRunning
    {
        get { return _isRunning; }
    }

    private bool _isAiming;
    public bool IsAiming
    {
        get { return _isAiming; }
    }

    private bool _isCombineShooting;
    public bool IsCombineShooting
    {
        get { return _isCombineShooting; }
    }

    private bool _isLeftShooting;
    public bool IsLeftShooting
    {
        get { return _isLeftShooting; }
    }

    private bool _isRightShooting;
    public bool IsRightShooting
    {
        get { return _isRightShooting; }
    }

    private bool _isJumping;
    public bool IsJumping
    {
        get { return _isJumping; }
    }

    private bool _isWaveAttacking;
    public bool IsWaveAttacking
    {
        get { return _isWaveAttacking; }
    }

    private bool _isDashing;
    public bool IsDashing
    {
        get { return _isDashing; }
    }

    private bool _isChangingLeftSpell;
    public bool IsChangingLeftSpell
    {
        get { return _isChangingLeftSpell; }
    }

    private bool _isChangingRightSpell;
    public bool IsChangingRightSpell
    {
        get { return _isChangingRightSpell; }
    }

    private bool _isChangingActiveElement;
    public bool IsChangingActiveElement
    {
        get { return _isChangingActiveElement; }
    }

    private bool _isMeleeAttacking;
    public bool IsMeleeAttacking
    {
        get { return _isMeleeAttacking; }
    }

    private bool _isInterating;
    public bool IsInteracting
    {
        get { return _isInterating; }
    }

    //Callbacks
    public Action OnLeftShootKeydown;
    public Action OnRightShootKeydown;
    public Action OnLeftShootKeyup;
    public Action OnRightShootKeyup;

    public Action OnMeleeAttackKeydown;
    public Action OnMeleeAttackKeyup;

    public Action OnInteration;
    public Action OnJumpKeyDown;
    public Action OnJumpKeyUp;

    public Action OnInventoryKeydown;
    public Action OnInventoryKeyup;
    public Action OnMissionKeydown;
    public Action OnMissionKeyup;
    public Action OnMenuKeydown;
    public Action OnMenuKeyup;

    // Start is called before the first frame update
    void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.PlayerMovement.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
        _playerInput.PlayerMovement.Move.canceled += ctx => Move(Vector2.zero);
        _playerInput.PlayerMovement.Run.performed += ctx => _isRunning = true;
        _playerInput.PlayerMovement.Run.canceled += ctx => _isRunning = false;
        _playerInput.PlayerMovement.Jump.performed += ctx =>
        {
            OnJumpKeyDown?.Invoke();
            _isJumping = true;
        };
        _playerInput.PlayerMovement.Jump.canceled += ctx =>
        {
            OnJumpKeyUp?.Invoke();
            _isJumping = false;
        };

        _playerInput.PlayerMovement.Dash.performed += ctx => _isDashing = true;
        _playerInput.PlayerMovement.Dash.canceled += ctx => _isDashing = false;
        
        // CLicka no F e entra no estado isInteracting
        _playerInput.PlayerMovement.Interact.performed += ctx => _isInterating = true;
        _playerInput.PlayerMovement.Interact.canceled += ctx => _isInterating = false;


        _playerInput.CameraControl.Look.performed += ctx => Look(ctx.ReadValue<Vector2>());
        _playerInput.CameraControl.Look.canceled += ctx => Look(Vector2.zero);

        _playerInput.Combat.Aim.performed += ctx => _isAiming = true;
        _playerInput.Combat.Aim.canceled += ctx => _isAiming = false;

        _playerInput.Combat.CombineSpellShot.performed += ctx => _isCombineShooting = true;
        _playerInput.Combat.CombineSpellShot.canceled += ctx => _isCombineShooting = false;

        _playerInput.Combat.LeftSpellShot.performed += ctx =>
        {
            _isLeftShooting = true;
            OnLeftShootKeydown?.Invoke();
        };
        _playerInput.Combat.LeftSpellShot.canceled += ctx =>
        {
            _isLeftShooting = false;
            OnLeftShootKeyup?.Invoke();
        };
        _playerInput.Combat.RightSpellShot.performed += ctx =>
        {
            _isRightShooting = true;
            OnRightShootKeydown?.Invoke();
        };
        _playerInput.Combat.RightSpellShot.canceled += ctx =>
        {
            _isRightShooting = false;
            OnRightShootKeyup?.Invoke();
        };

        _playerInput.Combat.ChangeLeftSpell.performed += ctx => _isChangingLeftSpell = true;
        _playerInput.Combat.ChangeLeftSpell.canceled += ctx => _isChangingLeftSpell = false;

        _playerInput.Combat.ChangeRightSpell.performed += ctx => _isChangingRightSpell = true;
        _playerInput.Combat.ChangeRightSpell.canceled += ctx => _isChangingRightSpell = false;

        _playerInput.Combat.ChangeActiveElement.performed += ctx => _isChangingActiveElement = true;
        _playerInput.Combat.ChangeActiveElement.canceled += ctx => _isChangingActiveElement = false;

        _playerInput.Combat.MeleeAttack.performed += ctx =>
        {
            _isMeleeAttacking = true;
            OnMeleeAttackKeydown?.Invoke();
        };
        _playerInput.Combat.MeleeAttack.canceled += ctx =>
        {
            _isMeleeAttacking = false;
            OnMeleeAttackKeyup?.Invoke();
        };

        _playerInput.UI.Inventory.performed += ctx => OnInventoryKeydown?.Invoke();
        _playerInput.UI.Inventory.canceled += ctx => OnInventoryKeyup?.Invoke();

        _playerInput.UI.Missions.performed += ctx => OnMissionKeydown?.Invoke();
        _playerInput.UI.Missions.canceled += ctx => OnMissionKeyup?.Invoke();

        _playerInput.UI.Menu.performed += ctx => OnMenuKeydown?.Invoke();
        _playerInput.UI.Menu.canceled += ctx => OnMenuKeyup?.Invoke();
    }

    private void Move(Vector2 value)
    {
        _moveInput = value;
    }

    private void Look(Vector2 value)
    {
        _lookInput = value;
    }

    void OnEnable()
    {
        _playerInput.Enable();
    }

    void OnDisable()
    {
        _playerInput.Disable();
    }
}
