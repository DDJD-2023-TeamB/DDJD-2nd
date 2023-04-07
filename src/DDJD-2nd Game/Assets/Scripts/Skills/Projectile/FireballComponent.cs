using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FireballComponent : ChargeProjectileComponent
{
    [SerializeField] 
    private float _explosionRadius = 3.0f;

    private float _currentRadius = 0.0f;
    private float _maxRadius = 0.5f;

    private VisualEffect _fireballVFX;

    protected override void OnImpact(Collider other){
        //Raycast sphere
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, _explosionRadius, Vector3.up, 0.0f);
        foreach(RaycastHit hit in hits){
            if(hit.collider.gameObject == _caster){
                continue;
            }
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if(rb != null){
                rb.AddExplosionForce(_stats.Damage * 5.0f, transform.position, _explosionRadius, 0.0f, ForceMode.Impulse);
            }
        }
    }

    override protected void Awake(){
        base.Awake();
        _fireballVFX = GetComponentInChildren<VisualEffect>();
    }

    protected override void OnCharge(){
        _currentRadius = Mathf.Lerp(0, _maxRadius, GetCurrentCharge());
        _fireballVFX.SetFloat("Size", _currentRadius);
        
    }
}
