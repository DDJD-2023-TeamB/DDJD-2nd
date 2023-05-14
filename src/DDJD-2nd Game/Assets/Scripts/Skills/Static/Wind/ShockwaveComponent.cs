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
        SkillComponent otherSkill = other.GetComponent<SkillComponent>();
        if (otherSkill?.Caster == _caster)
        {
            return;
        }
        Vector3 direction = _shootDirection.normalized;
        Rigidbody rb = other.GetComponent<Rigidbody>();
        float force = _stats.ForceWithDamage() * multiplier;
        //Redirect skill

        if (otherSkill != null)
        {
            otherSkill.SetCaster(_caster);
            rb.velocity = Vector3.zero;
        }
        if (rb != null)
        {
            rb.AddForce(force * direction, ForceMode.Impulse);
        }

        Damage(
            other.gameObject,
            (int)(_stats.Damage * multiplier),
            (int)force,
            transform.position,
            direction
        );

        //Knockdown enemy
        BasicEnemy enemy = other.GetComponent<BasicEnemy>();
        enemy?.Knockdown(force, transform.position, direction);
    }

    public override void DestroySpell()
    {
        DeactivateSpell();
        Destroy(gameObject, 2.0f);
    }
}
