using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : ProjectileComponent
{
    [SerializeField]
    private float _force = 30f;

    protected override void Awake()
    {
        base.Awake();
        _destroyOnImpact = false;
    }

    protected override void OnImpact(Collider other)
    {
        Vector3 direction = (other.transform.position - transform.position).normalized;
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(_stats.Damage * _force * direction, ForceMode.Impulse);
        }

        Damageable damageable = other.GetComponent<Damageable>();
        if (damageable != null)
        {
            damageable.TakeDamage((int)_stats.Damage, transform.position, direction);
        }
    }
}
