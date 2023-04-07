using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileComponent : MonoBehaviour , SkillComponent
{

    private Rigidbody _rb;
    protected ProjectileStats _stats;
    protected Projectile _skill;
    protected GameObject _impactPrefab;

    protected GameObject _caster;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    

    public void SetCaster(GameObject caster){
        _caster = caster;
    }
    public void SetSkill(Skill skill){
        _skill = (Projectile) skill;
        _stats = _skill.ProjectileStats;
        _impactPrefab = _skill.ImpactPrefab;
    }

    public virtual void Shoot(Vector3 direction){
        _rb.AddForce(direction.normalized * _stats.Speed , ForceMode.Impulse);
    }



    public void OnTriggerEnter(Collider other){
        if(other.gameObject == _caster){
            return;
        }
        if(_impactPrefab != null){
            GameObject impact = Instantiate(_impactPrefab, transform.position, Quaternion.identity);
            Destroy(impact, 3.0f);
        }
        OnImpact(other);
        Destroy(gameObject);
    }

    protected abstract void OnImpact(Collider other);
}
