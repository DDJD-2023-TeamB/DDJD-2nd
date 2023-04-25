using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillComponent : MonoBehaviour
{
    protected GameObject _caster;

    protected bool _isChargeAttack = false;
    protected ChargeComponent _chargeComponent;

    virtual protected void Awake()
    {
        Debug.Log("SkillComponent Awake");
        _chargeComponent = GetComponent<ChargeComponent>();
        if (_chargeComponent == null)
        {
            Debug.LogError(gameObject.name + ": No ChargeComponent found when required");
            return;
        }
    }

    public GameObject Caster
    {
        get { return _caster; }
    }

    [SerializeField]
    private bool _damageCaster = false;

    protected float _elapsedTime = 0.0f;

    virtual protected void Update()
    {
        _elapsedTime += Time.deltaTime;
    }

    public virtual void SetCaster(GameObject caster)
    {
        _caster = caster;
    }

    public virtual void SetSkill(Skill skill)
    {
        SkillStats stats = skill.Stats;
        if (stats.CastType == CastType.Charge)
        {
            _isChargeAttack = true;
            _chargeComponent.MaxChargeTime = stats.MaxChargeTime;
            _chargeComponent.MinChargeTime = stats.MinChargeTime;
        }
    }

    protected void Damage(GameObject target, int damage, Vector3 hitPoint, Vector3 direction)
    {
        Damageable damageable = target.GetComponent<Damageable>();
        if (!_damageCaster && target == _caster)
        {
            return;
        }
        if (damageable == null)
        {
            return;
        }

        damageable.TakeDamage(damage, hitPoint, direction);
    }

    public virtual void Shoot(Vector3 direction)
    {
        // Do nothing
    }
}
