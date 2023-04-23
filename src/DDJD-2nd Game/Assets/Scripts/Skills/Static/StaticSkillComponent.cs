using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StaticSkillComponent : SkillComponent
{
    private Collider _collider;

    protected virtual void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    protected StaticSkillStats _stats;
    protected StaticSkill _skill;

    protected Vector3 _shootDirection;

    private Dictionary<GameObject, float> _collidedObjects = new Dictionary<GameObject, float>();

    public override void SetSkill(Skill skill)
    {
        base.SetSkill(skill);
        _skill = (StaticSkill)skill;
        _stats = _skill.StaticStats;
    }

    public override void Shoot(Vector3 direction)
    {
        transform.parent = null; // Detach from caster
        _shootDirection = direction;
        StartCoroutine(DestroyAfterTime(_stats.Duration));
    }

    public void OnTriggerEnter(Collider other)
    {
        Collide(other);
    }

    public void OnTriggerStay(Collider other)
    {
        if (_stats == null || !_stats.IsContinuous)
        {
            return;
        }
        Collide(other);
    }

    protected virtual void OnImpact(Collider other, float multiplier = 1)
    {
        // Override this method to add functionality
    }

    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        _collider.enabled = false;
        Destroy(gameObject, 1.0f);
    }

    private void Collide(Collider other)
    {
        if (!CanCollide(other))
        {
            return;
        }

        GameObject otherObject = other.gameObject;
        if (_stats.IsContinuous)
        {
            if (_collidedObjects.ContainsKey(otherObject))
            {
                // Register new impact
                if (_elapsedTime - _collidedObjects[otherObject] > _stats.TickRate)
                {
                    _collidedObjects[otherObject] = _elapsedTime;
                    float multiplier =
                        _stats.TickRate / (_elapsedTime - _collidedObjects[otherObject]);
                    OnImpact(other, multiplier);
                }
            }
            else
            {
                // Register first impact with object
                _collidedObjects.Add(otherObject, _elapsedTime);
                OnImpact(other, 1f);
            }
        }
        else if (!_collidedObjects.ContainsKey(otherObject))
        {
            // Register first impact with object
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
}
