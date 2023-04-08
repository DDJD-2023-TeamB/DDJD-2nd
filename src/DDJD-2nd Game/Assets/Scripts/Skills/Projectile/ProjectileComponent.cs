using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileComponent : MonoBehaviour , SkillComponent
{

    protected Rigidbody _rb;
    protected Collider _collider;
    protected ProjectileStats _stats;
    protected Projectile _skill;
    protected GameObject _impactPrefab;

    protected GameObject _caster;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        DeactivateSpell();
    }

    void DeactivateSpell(){
        _collider.enabled = false;
        _rb.isKinematic = true;
    }

    void ActivateSpell(){
        _collider.enabled = true;
        _rb.isKinematic = false;
    }    

    public void SetCaster(GameObject caster){
        _caster = caster;
    }
    public virtual void SetSkill(Skill skill){
        _skill = (Projectile) skill;
        _stats = _skill.ProjectileStats;
        _impactPrefab = _skill.ImpactPrefab;
    }

    public virtual void Shoot(Vector3 direction){
        ActivateSpell();
        transform.parent = null; // Detach from caster
        _rb.AddForce(direction.normalized * _stats.Speed , ForceMode.Impulse);
        
    }



    public void OnTriggerEnter(Collider other){
        if(other.gameObject == _caster){
            return;
        }
        if(_impactPrefab != null){
            SpawnHitVFX();
        }
        OnImpact(other);
        Destroy(gameObject);
    }

    protected virtual void SpawnHitVFX(){
        GameObject impact = Instantiate(_impactPrefab, transform.position, Quaternion.identity);
        Destroy(impact, 3.0f);
    }
    protected abstract void OnImpact(Collider other);
}
