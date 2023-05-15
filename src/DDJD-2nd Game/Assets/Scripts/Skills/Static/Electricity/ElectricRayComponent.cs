using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ElectricRayComponent : StaticSkillComponent, NonCollidable
{
    private VisualEffect _vfx;

    protected override void Awake()
    {
        base.Awake();
        _vfx = GetComponent<VisualEffect>();
    }

    protected override void Update()
    {
        base.Update();
        RaycastHit? hit = SetRayPosition();
        if (hit != null)
            Collide(hit.Value.collider);
    }

    private RaycastHit? SetRayPosition()
    {
        Vector3 pos4;
        RaycastHit? hit = GetRaycastHit();
        if (hit != null)
        {
            pos4 = hit.Value.point;
        }
        else
        {
            pos4 = transform.position + transform.forward * 20;
        }
        Vector3 pos1 = transform.position;
        ElectricRayUtils.SetBezierAndScale(transform, pos1, pos4);
        return hit;
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
        _vfx.Stop();
        Destroy(gameObject);
    }

    private RaycastHit? GetRaycastHit()
    {
        AimComponent aimComponent = _caster.GetComponent<AimComponent>();
        RaycastHit hit;
        if (aimComponent.GetAimRaycastHit(out hit))
        {
            return hit;
        }
        return null;
    }
}
