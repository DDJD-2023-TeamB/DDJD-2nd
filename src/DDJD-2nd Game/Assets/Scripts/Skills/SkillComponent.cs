using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillComponent : MonoBehaviour
{
    protected GameObject _caster;
    protected SkillStats _skillStats;

    protected bool _isChargeAttack = false;
    protected ChargeComponent _chargeComponent;
    protected NoiseSource _noiseComponent;

    virtual protected void Awake()
    {
        _chargeComponent = GetComponent<ChargeComponent>();
        _noiseComponent = GetComponent<NoiseSource>();
    }

    public GameObject Caster
    {
        get { return _caster; }
    }

    [SerializeField]
    private bool _damageCaster = false;

    protected float _elapsedTime = 0.0f;

    protected virtual void Update()
    {
        _elapsedTime += Time.deltaTime;
    }

    public virtual void SetCaster(GameObject caster)
    {
        _caster = caster;
    }

    private Dictionary<GameObject, float> _collidedObjects = new Dictionary<GameObject, float>();

    public virtual void SetSkill(Skill skill)
    {
        _skillStats = skill.SkillStats;
        if (_skillStats.CastType == CastType.Charge)
        {
            if (_chargeComponent == null)
            {
                Debug.LogError(gameObject.name + ": No ChargeComponent found when required");
                return;
            }
            _isChargeAttack = true;
            _chargeComponent.MaxChargeTime = _skillStats.MaxChargeTime;
            _chargeComponent.MinChargeTime = _skillStats.MinChargeTime;
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
        damageable.TakeDamage(damage, _skillStats.ForceWithDamage(), hitPoint, direction);
    }

    public virtual void Shoot(Vector3 direction)
    {
        // Do nothing
    }

    public virtual void Aim(Vector3 direction)
    {
        transform.rotation = Quaternion.LookRotation(direction);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (_skillStats == null)
        {
            return;
        }
        if (Collide(other))
        {
            _noiseComponent?.MakeNoise(GetNoiseLevel());
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        OnImpact(collision.collider);
        _noiseComponent?.MakeNoise(GetNoiseLevel());
    }

    public virtual void OnTriggerStay(Collider other)
    {
        if (_skillStats == null || !_skillStats.IsContinuous)
        {
            return;
        }
        Collide(other);
    }

    protected virtual void OnImpact(Collider other, float multiplier = 1)
    {
        // Override this method to add functionality
    }

    private bool Collide(Collider other)
    {
        if (!CanCollide(other))
        {
            return false;
        }
        GameObject otherObject = other.gameObject;
        if (_skillStats.IsContinuous)
        {
            if (_collidedObjects.ContainsKey(otherObject))
            {
                // Register new impact
                if (_elapsedTime - _collidedObjects[otherObject] > _skillStats.TickRate)
                {
                    float multiplier =
                        _skillStats.TickRate / (_elapsedTime - _collidedObjects[otherObject]);
                    _collidedObjects[otherObject] = _elapsedTime;
                    multiplier = Mathf.Max(multiplier, 1f);
                    OnImpact(other, multiplier);
                }
            }
            else
            {
                _collidedObjects.Add(otherObject, _elapsedTime);
                OnImpact(other, 1f);
            }
        }
        else if (!_collidedObjects.ContainsKey(otherObject))
        {
            _collidedObjects.Add(otherObject, _elapsedTime);
            OnImpact(other, 1f);
        }
        return true;
    }

    private bool CanCollide(Collider other)
    {
        if (other.gameObject == _caster)
        {
            return false;
        }
        if (_caster == null || other.GetComponent<NonCollidable>() != null)
        {
            return false;
        }
        SkillComponent skillComponent = other.GetComponent<SkillComponent>();
        if (skillComponent != null && skillComponent.Caster.layer == _caster.layer)
        {
            return false;
        }
        return true;
    }

    public virtual void DestroySpell()
    {
        Destroy(gameObject);
    }

    public virtual bool CanShoot(Vector3 direction)
    {
        return true;
    }

    public virtual float GetNoiseLevel()
    {
        return _skillStats.NoiseLevel;
    }
}
