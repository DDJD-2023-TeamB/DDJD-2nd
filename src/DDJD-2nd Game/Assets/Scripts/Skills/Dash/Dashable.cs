using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public abstract class Dashable : MonoBehaviour
{
    protected Rigidbody _rigidbody;
    protected Animator _animator;

    [SerializeField]
    // This cooldown is used to prevent the character from dashing too often, even if the skill was used or its cooldown is over
    protected float _minCooldown = 1f;
    protected float _timeSinceLastDash = 0f;

    [SerializeField]
    protected bool allowAllDirections = true;

    [SerializeField]
    protected bool disableGravity = false;

    [SerializeField]
    protected bool resetVelocity = true;

    [SerializeField]
    protected float _maxRegularSpeed = 5f;

    [SerializeField]
    protected float _speedChangeFactor = 50f;

    protected float _maxSpeed;
    protected bool _isDashing = false;
    public bool IsDashing
    {
        get => _isDashing;
    }
    protected DashStats _currentDashStats;

    private DashComponent _lastDashSkill;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _maxSpeed = _maxRegularSpeed;
    }

    protected void Update()
    {
        Vector3 flatVel = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
        if (flatVel.magnitude > _maxSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * _maxSpeed;
            _rigidbody.velocity = new Vector3(limitedVel.x, _rigidbody.velocity.y, limitedVel.z);
        }
        _timeSinceLastDash += Time.deltaTime;
    }

    public virtual void DashWithSkill(DashSkill dashSkill)
    {
        if (!Dash(dashSkill.DashStats))
            return;

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
        dashComponent.SetDashDirection(direction);
        _lastDashSkill = dashComponent;
    }

    /**
    * Returns true if the dash was successful, false otherwise
    */
    public virtual bool Dash(DashStats stats)
    {
        if (_timeSinceLastDash < _minCooldown)
            return false;
        _currentDashStats = stats;
        _isDashing = true;
        Vector3 direction = GetDashDirection();
        Vector3 force = direction * stats.Force;

        if (disableGravity)
            _rigidbody.useGravity = false;
        if (resetVelocity)
            _rigidbody.velocity = Vector3.zero;

        _rigidbody.AddForce(force * 100f, ForceMode.Acceleration);
        SetDashAnimation();

        _maxSpeed = stats.MaxSpeed;
        StartCoroutine(SmoothlyChangeMaxSpeed(stats.MaxSpeed, false));

        Invoke(nameof(ResetDash), stats.Duration);

        _timeSinceLastDash = 0f;
        return true;
    }

    protected virtual void ResetDash()
    {
        if (disableGravity)
            _rigidbody.useGravity = true;
        _isDashing = false;
        _currentDashStats = null;

        _animator.SetBool("IsDashing", false);
        _animator.SetFloat("DashX", 0f);
        _animator.SetFloat("DashY", 0f);
        StartCoroutine(SmoothlyChangeMaxSpeed(_maxRegularSpeed, true));
    }

    protected IEnumerator SmoothlyChangeMaxSpeed(float targetSpeed, bool isEnd = false)
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
        if (isEnd)
        {
            _lastDashSkill?.OnDashEnd();
            _lastDashSkill = null;
        }
        _maxSpeed = targetSpeed;
    }

    protected abstract Vector3 GetDashDirection();

    protected abstract void SetDashAnimation();

    public bool IsDashOnCooldown()
    {
        return _timeSinceLastDash < _minCooldown;
    }
}
