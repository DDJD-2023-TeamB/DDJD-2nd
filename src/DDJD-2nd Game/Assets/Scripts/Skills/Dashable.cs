using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashable : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private PlayerInputReceiver _inputReceiver;

    [SerializeField]
    private Transform _playerCam;

    [SerializeField]
    private bool allowAllDirections = true;

    [SerializeField]
    private bool disableGravity = false;

    [SerializeField]
    private bool resetVelocity = true;

    [SerializeField]
    private float _maxRegularSpeed = 5f;

    private bool _isDashing = false;
    public bool IsDashing
    {
        get => _isDashing;
    }
    private DashStats _currentDashStats;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _inputReceiver = GetComponent<PlayerInputReceiver>();
    }

    private void Update()
    {
        Vector3 flatVel = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
        float maxSpeed = _isDashing ? _currentDashStats.MaxSpeed : _maxRegularSpeed;
        if (flatVel.magnitude > maxSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * maxSpeed;
            _rigidbody.velocity = new Vector3(limitedVel.x, _rigidbody.velocity.y, limitedVel.z);
        }
    }

    public void DashWithSkill(DashSkill dashSkill)
    {
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

        Dash(dashSkill.DashStats);
    }

    public void Dash(DashStats stats)
    {
        Vector3 direction = GetDashDirection();
        Vector3 force = direction * stats.Force;

        if (disableGravity)
            _rigidbody.useGravity = false;
        if (resetVelocity)
            _rigidbody.velocity = Vector3.zero;

        _rigidbody.AddForce(force, ForceMode.Impulse);

        _isDashing = true;
        _currentDashStats = stats;
        Invoke(nameof(ResetDash), stats.Duration);
    }

    private void ResetDash()
    {
        if (disableGravity)
            _rigidbody.useGravity = true;
        _isDashing = false;
        _currentDashStats = null;
    }

    private Vector3 GetDashDirection()
    {
        Transform forwardTransform;
        if (allowAllDirections)
            forwardTransform = _playerCam;
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
}
