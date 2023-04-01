using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballComponent : ProjectileComponent
{
    [SerializeField] 
    private float _radius = 3.0f;
    protected override void OnImpact(Collider other){
        //Raycast sphere
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, _radius, Vector3.up, 0.0f);
        foreach(RaycastHit hit in hits){
            if(hit.collider.gameObject == _caster){
                continue;
            }
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if(rb != null){
                rb.AddExplosionForce(_stats.Damage * 5.0f, transform.position, _radius, 0.0f, ForceMode.Impulse);
            }
        }
    }
}
