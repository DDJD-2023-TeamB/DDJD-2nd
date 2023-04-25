using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillComponent : MonoBehaviour
{
    protected GameObject _caster;
    protected SkillStats _skillStats;
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

    public void SetCaster(GameObject caster)
    {
        _caster = caster;
    }

    public virtual void SetSkill(Skill skill)
    {
        _skillStats = skill.SkillStats;
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
        Debug.Log("Damage: " + damage);
        damageable.TakeDamage(damage, hitPoint, direction);
    }

    public virtual void Shoot(Vector3 direction)
    {
        // Do nothing
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (_skillStats == null)
        {
            return;
        }
        Collide(other);
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

    private void Collide(Collider other)
    {
        if (!CanCollide(other))
        {
            return;
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
                // Register first impact with object
                Debug.Log("First impact");
                _collidedObjects.Add(otherObject, _elapsedTime);
                OnImpact(other, 1f);
            }
        }
        else if (!_collidedObjects.ContainsKey(otherObject))
        {
            // Register first impact with object
            Debug.Log("First impact");
            _collidedObjects.Add(otherObject, _elapsedTime);
            OnImpact(other, 1f);
        }
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
        return true;
    }

    private Dictionary<GameObject, float> _collidedObjects = new Dictionary<GameObject, float>();
}
