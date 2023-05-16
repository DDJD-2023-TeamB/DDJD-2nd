using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FlameThrowerComponent : StaticSkillComponent, NonCollidable
{
    private VisualEffect _vfx;

    protected override void Awake()
    {
        base.Awake();
        _vfx = GetComponent<VisualEffect>();
    }

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
            (int)(_skillStats.ForceWithDamage() * multiplier),
            other.ClosestPoint(_caster.transform.position),
            _caster.transform.forward
        );
    }

    public override void DestroySpell()
    {
        Debug.Log("Destroying FlameThrower");
        DeactivateSpell();
        _vfx.Stop();
        Destroy(gameObject, 3.0f);
    }
}
