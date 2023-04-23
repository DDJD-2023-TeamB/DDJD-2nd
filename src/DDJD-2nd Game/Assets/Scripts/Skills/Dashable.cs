using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Dashable : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private PlayerInputReceiver _inputReceiver;
    private Animator _animator;

    [SerializeField]
    private Transform _playerCamTransform;
    private FovController _fovController;

    [SerializeField]
    private bool allowAllDirections = true;

    [SerializeField]
    private bool disableGravity = false;

    [SerializeField]
    private bool resetVelocity = true;

    [SerializeField]
    private float _maxRegularSpeed = 5f;

    [SerializeField]
    private float _speedChangeFactor = 50f;

    private float _maxSpeed;
    private bool _isDashing = false;
    public bool IsDashing
    {
        get => _isDashing;
    }
    private DashStats _currentDashStats;
    private float _defaultFov;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _inputReceiver = GetComponent<PlayerInputReceiver>();
        _animator = GetComponent<Animator>();
        _maxSpeed = _maxRegularSpeed;
        _fovController = _playerCamTransform.GetComponent<FovController>();
        _defaultFov = _fovController.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView;
    }

    private void Update()
    {
        Vector3 flatVel = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
        if (flatVel.magnitude > _maxSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * _maxSpeed;
            _rigidbody.velocity = new Vector3(limitedVel.x, _rigidbody.velocity.y, limitedVel.z);
        }
    }

    public void DashWithSkill(DashSkill dashSkill)
    {
        Dash(dashSkill.DashStats);

        Vector3 direction = GetDashDirection();
        Vector3 position = new Vector3(
            transform.position.x,
            transform.position.y + 1f,
            transform.position.z
        );
        Quaternion rotation = Quaternion.LookRotation(direction);

        GameObject spell = Instantiate(dashSkill.SpellPrefab, position, rotation);
        DashComponent dashComponent = spell.GetComponent<DashComponent>();
        dashComponent.SetCaster(gameObject);
        dashComponent.SetSkill(dashSkill);
    }

    public void Dash(DashStats stats)
    {
        _isDashing = true;
        Vector3 direction = GetDashDirection();
        Vector3 force = direction * stats.Force;

        if (disableGravity)
            _rigidbody.useGravity = false;
        if (resetVelocity)
            _rigidbody.velocity = Vector3.zero;

        _rigidbody.AddForce(force, ForceMode.Impulse);
        SetDashAnimation();
        _fovController.ChangeFov(stats.CameraFov, stats.EnterFovTime);

        _maxSpeed = stats.MaxSpeed;
        _currentDashStats = stats;
        Invoke(nameof(ResetDash), stats.Duration);
    }

    private void ResetDash()
    {
        if (disableGravity)
            _rigidbody.useGravity = true;
        _isDashing = false;
        _fovController.ChangeFov(_defaultFov, _currentDashStats.ExitFovTime);
        _currentDashStats = null;

        _animator.SetBool("IsDashing", false);
        _animator.SetFloat("DashX", 0f);
        _animator.SetFloat("DashY", 0f);
        StartCoroutine(SmoothlyChangeMaxSpeed(_maxRegularSpeed));
    }

    private IEnumerator SmoothlyChangeMaxSpeed(float targetSpeed)
    {
        float time = 0;
        float diff = Mathf.Abs(_maxSpeed - targetSpeed);
        float startValue = _maxSpeed;

        while (time < 1)
        {
            time += Time.deltaTime / diff * _speedChangeFactor;
            _maxSpeed = Mathf.Lerp(startValue, targetSpeed, time);
            yield return null;
        }

        _maxSpeed = targetSpeed;
    }

    private Vector3 GetDashDirection()
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
            direction =
                forwardTransform.forward * moveInput.y + forwardTransform.right * moveInput.x;
        }
        else
        {
            direction = forwardTransform.forward;
        }
        direction.y = 0;

        return direction.normalized;
    }

    private void SetDashAnimation()
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
}
