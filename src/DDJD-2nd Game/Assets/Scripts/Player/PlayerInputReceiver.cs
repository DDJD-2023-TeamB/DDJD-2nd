using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputReceiver : MonoBehaviour
{
    private PlayerInput _playerInput;

    private Vector2 _moveInput;
    public Vector2 MoveInput{get{return _moveInput;}}

    private Vector2 _lookInput;
    public Vector2 LookInput{get{return _lookInput;}}

    private bool _isRunning;
    public bool IsRunning{get{return _isRunning;}}

    private bool _isAiming;
    public bool IsAiming{get{return _isAiming;}}

    private bool _isShooting;
    public bool IsShooting{get{return _isShooting;}}

    private bool _isJumping;
    public bool IsJumping{get{return _isJumping;}}

    private bool _isWaveAttacking;
    public bool IsWaveAttacking{get{return _isWaveAttacking;}}

    private bool _isDashing;
    public bool IsDashing{get{return _isDashing;}}

    // Start is called before the first frame update
    void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.PlayerMovement.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
        _playerInput.PlayerMovement.Move.canceled += ctx => Move(Vector2.zero);
        _playerInput.PlayerMovement.Run.performed += ctx => _isRunning = true;
        _playerInput.PlayerMovement.Run.canceled += ctx => _isRunning = false;
        _playerInput.PlayerMovement.Jump.performed += ctx => _isJumping = true;
        _playerInput.PlayerMovement.Jump.canceled += ctx => _isJumping = false;

        _playerInput.PlayerMovement.Dash.performed += ctx => _isDashing = true;
        _playerInput.PlayerMovement.Dash.canceled += ctx => _isDashing = false;

        _playerInput.CameraControl.Look.performed += ctx => Look(ctx.ReadValue<Vector2>());
        _playerInput.CameraControl.Look.canceled += ctx => Look(Vector2.zero);
        
        _playerInput.Combat.Aim.performed += ctx => _isAiming = true;
        _playerInput.Combat.Aim.canceled += ctx => _isAiming = false;

        _playerInput.Combat.Shoot.performed += ctx => _isShooting = true;
        _playerInput.Combat.Shoot.canceled += ctx => _isShooting = false;


        
    }

    private void Move(Vector2 value)
    {
        _moveInput = value;
    }

    private void Look(Vector2 value){
        _lookInput = value;
    }


    void OnEnable(){
        _playerInput.Enable();
    }

    void OnDisable(){
        _playerInput.Disable();
    }
    
}
