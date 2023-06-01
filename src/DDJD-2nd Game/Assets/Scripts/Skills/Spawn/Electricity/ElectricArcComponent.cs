using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricArcComponent : SkillComponent
{
    protected override void OnImpact(Collider other, float multiplier = 1)
    {
        base.OnImpact(other, multiplier);
        Damage(
            other.gameObject,
            (int)(_skillStats.Damage * multiplier),
            (int)(_skillStats.ForceWithDamage() * multiplier),
            other.ClosestPoint(transform.position),
            other.transform.forward
        );
    }
}
