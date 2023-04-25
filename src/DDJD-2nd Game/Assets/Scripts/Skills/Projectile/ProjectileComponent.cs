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

    protected bool _leftCaster = false;

    [SerializeField]
    [Tooltip("Set velocity instead of adding force")]
    private bool _setVelocity = false;

    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        DeactivateSpell();
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

    public void OnTriggerEnter(Collider other)
    {
        // Remake this
        if (other.gameObject == _caster)
        {
            return;
        }

        if (other.GetComponent<NonCollidable>() != null)
        {
            return;
        }
        if (_impactPrefab != null)
        {
            SpawnHitVFX();
        }
        OnImpact(other);
        if (_stats.DestroyOnImpact)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void SpawnHitVFX()
    {
        GameObject impact = Instantiate(_impactPrefab, transform.position, Quaternion.identity);
        Destroy(impact, 3.0f);
    }

    protected abstract void OnImpact(Collider other);

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
