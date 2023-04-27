using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShotComponent : ProjectileComponent
{
    protected override void OnImpact(Collider other, float multiplier = 1)
    {
        base.OnImpact(other);

        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(transform.forward * _skillStats.ForceWithDamage(), ForceMode.Impulse);
        }

        Damage(
            other.gameObject,
            (int)(_skillStats.Damage * multiplier),
            other.ClosestPoint(_caster.transform.position),
            _caster.transform.forward
        );
    }
}
