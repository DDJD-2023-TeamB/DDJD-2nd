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

    [SerializeField]
    protected bool _destroyOnImpact = true;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        DeactivateSpell();
    }

    void DeactivateSpell()
    {
        _collider.enabled = false;
        _rb.isKinematic = true;
    }

    void ActivateSpell()
    {
        _collider.enabled = true;
        _rb.isKinematic = false;
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
        _rb.AddForce(direction.normalized * _stats.Speed, ForceMode.Impulse);
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (_destroyOnImpact)
        {
            Destroy(gameObject);
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
}
