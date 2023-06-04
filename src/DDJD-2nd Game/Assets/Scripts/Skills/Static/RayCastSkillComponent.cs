using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class RayCastSkillComponent : StaticSkillComponent, NonCollidable
{
    protected AimComponent _aimComponent;

    public override void SetCaster(GameObject caster)
    {
        base.SetCaster(caster);
        _aimComponent = caster.GetComponent<AimComponent>();
    }

    protected RaycastHit? GetRaycastHit()
    {
        RaycastHit hit;
        if (_aimComponent.GetAimRaycastHit(out hit))
        {
            return hit;
        }
        return null;
    }

    protected Vector3 GetRaycastShotDirection()
    {
        return _aimComponent.GetAimDirection(transform.position);
    }
}
