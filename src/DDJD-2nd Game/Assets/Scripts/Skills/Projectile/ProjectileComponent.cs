using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileComponent : SkillComponent
{
    protected Rigidbody _rb;
    protected Collider _collider;
    protected ProjectileStats _stats;
    protected Projectile _skill;
    protected GameObject _impactPrefab;

    private bool _originalIsKinematic;

    [SerializeField]
    protected bool _destroyOnImpact = true;

    protected bool _leftCaster = false;

    [SerializeField]
    [Tooltip("Set velocity instead of adding force")]
    private bool _setVelocity = false;

    private Vector3 _initialPosition;

    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        DeactivateSpell();
    }

    protected override void Update()
    {
        base.Update();
        if (Vector3.Distance(_initialPosition, transform.position) > _stats.Range)
        {
            DestroySpell();
        }
    }

    protected void DeactivateSpell()
    {
        _originalIsKinematic = _rb.isKinematic;
        _collider.enabled = false;
        _rb.isKinematic = true;
    }

    protected void ActivateSpell()
    {
        _collider.enabled = true;
        _rb.isKinematic = _originalIsKinematic;
    }

    public override void SetSkill(Skill skill)
    {
        base.SetSkill(skill);
        _skill = (Projectile)skill;
        _stats = _skill.ProjectileStats;
        _impactPrefab = _skill.ImpactPrefab;
    }

    public override void Shoot(Vector3 direction)
    {
        ActivateSpell();
        _initialPosition = transform.position;
        transform.parent = null; // Detach from caster
        _leftCaster = true;
        if (_setVelocity)
        {
            _rb.velocity = direction.normalized * _stats.Speed;
        }
        else
        {
            _rb.AddForce(direction.normalized * _stats.Speed, ForceMode.Acceleration);
        }

        if (_isChargeAttack)
        {
            _chargeComponent.StopCharging();
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    protected override void OnImpact(Collider other, float multiplier = 1)
    {
        base.OnImpact(other, multiplier);
        if (_destroyOnImpact)
        {
            DestroySpell();
            SpawnHitVFX();
        }
    }

    protected virtual void SpawnHitVFX()
    {
        if (_impactPrefab == null)
        {
            return;
        }
        GameObject impact = Instantiate(_impactPrefab, transform.position, Quaternion.identity);
        Destroy(impact, 3.0f);
    }

    protected float GetDamage()
    {
        if (_isChargeAttack)
        {
            return _stats.Damage * _chargeComponent.GetCurrentCharge();
        }
        return _stats.Damage;
    }

    protected float GetForce()
    {
        if (_isChargeAttack)
        {
            return _stats.ForceWithDamage() * _chargeComponent.GetCurrentCharge();
        }
        return _stats.ForceWithDamage();
    }
}
