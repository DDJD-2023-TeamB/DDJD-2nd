using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveComponent : StaticSkillComponent, NonCollidable
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnImpact(Collider other, float multiplier = 1)
    {
        // Don't collide with skills from caster
        if (other.GetComponent<SkillComponent>()?.Caster == _caster)
        {
            return;
        }
        Vector3 direction = (other.transform.position - _caster.transform.position).normalized;
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(
                _stats.Damage * _stats.ForceWithDamage() * direction * multiplier,
                ForceMode.Impulse
            );
        }

        Damageable damageable = other.GetComponent<Damageable>();
        if (damageable != null)
        {
            damageable.TakeDamage((int)(_stats.Damage * multiplier), transform.position, direction);
        }
    }
}
