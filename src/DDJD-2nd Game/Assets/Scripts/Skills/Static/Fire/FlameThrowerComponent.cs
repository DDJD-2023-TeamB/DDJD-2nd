using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerComponent : StaticSkillComponent, NonCollidable
{
    public override void Shoot(Vector3 direction)
    {
        base.Shoot(direction);
        //face the direction of the caster
        transform.rotation = Quaternion.LookRotation(direction);
    }

    protected override void OnImpact(Collider other, float multiplier = 1)
    {
        base.OnImpact(other);
        Damage(
            other.gameObject,
            (int)(_skillStats.Damage * multiplier),
            other.ClosestPoint(_caster.transform.position),
            _caster.transform.forward
        );
    }
}
