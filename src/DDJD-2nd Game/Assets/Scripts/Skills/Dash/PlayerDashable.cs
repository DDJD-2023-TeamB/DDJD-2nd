using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerDashable : Dashable
{
    private PlayerInputReceiver _inputReceiver;

    [SerializeField]
    private Transform _playerCamTransform;
    private FovController _fovController;
    private float _defaultFov;

    private Player _player;

    protected CharacterMovement _characterMovement;

    protected override void Awake()
    {
        base.Awake();
        _inputReceiver = GetComponent<PlayerInputReceiver>();
        _fovController = _playerCamTransform.GetComponent<FovController>();
        _defaultFov = _fovController.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView;
        _player = GetComponent<Player>();
        _characterMovement = GetComponent<CharacterMovement>();
    }

    protected void Start()
    {
        _maxRegularSpeed = _characterMovement.GetCurrentMaxSpeed();
        _maxSpeed = _maxRegularSpeed;
    }

    /**
    * Returns true if the dash was successful, false otherwise
    */
    public override bool Dash(DashStats stats)
    {
        bool dashSuccessful = base.Dash(stats);
        if (!dashSuccessful)
            return false;
        _player.CameraController.ShakeCamera(stats.CameraShakeIntensity, stats.CameraShakeDuration);
        _fovController.ChangeFov(stats.CameraFov, stats.EnterFovTime);
        return true;
    }

    protected override void ResetDash()
    {
        _fovController.ChangeFov(_defaultFov, _currentDashStats.ExitFovTime);
        base.ResetDash();
    }

    protected override Vector3 GetDashDirection()
    {
        Transform forwardTransform;
        if (allowAllDirections)
            forwardTransform = _playerCamTransform;
        else
            forwardTransform = transform;

        Vector2 moveInput = _inputReceiver.MoveInput;
        Vector3 direction = Vector3.zero;
        if (allowAllDirections && moveInput != Vector2.zero)
        {
            direction = transform.forward * moveInput.y + transform.right * moveInput.x;
        }
        else
        {
            direction = forwardTransform.forward;
        }
        Vector3 axis =
            -forwardTransform.forward * moveInput.x + forwardTransform.right * moveInput.y;
        float angle = _currentDashStats.Angle;
        direction = Quaternion.AngleAxis(-angle, axis) * direction;
        return direction.normalized;
    }

    protected override void SetDashAnimation()
    {
        Vector2 moveInput = _inputReceiver.MoveInput;
        float dashX = 0;
        float dashY = 0;

        if (moveInput.x != 0)
            dashX = moveInput.x > 0 ? 1 : -1;
        if (moveInput.y != 0)
            dashY = moveInput.y > 0 ? 1 : -1;

        _animator.SetBool("IsDashing", true);
        _animator.SetFloat("DashX", dashX);
        _animator.SetFloat("DashY", dashY);
    }

    protected override float GetRegularSpeed()
    {
        return _characterMovement.GetCurrentMaxSpeed();
    }

    public void UpdateMaxSpeed(float maxSpeed)
    {
        _maxRegularSpeed = maxSpeed;
        if (!_isDashing)
            _maxSpeed = maxSpeed;
    }
}
