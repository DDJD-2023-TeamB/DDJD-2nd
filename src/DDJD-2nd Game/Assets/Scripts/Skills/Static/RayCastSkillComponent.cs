using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class RayCastSkillComponent : StaticSkillComponent, NonCollidable
{
    protected RaycastHit? GetRaycastHit()
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
