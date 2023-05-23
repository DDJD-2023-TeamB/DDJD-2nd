using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ElectricRayComponent : RayCastSkillComponent, NonCollidable
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
        SetRayPosition();
    }

    private void SetRayPosition()
    {
        Vector3 pos1 = transform.position;
        Vector3 pos4;
        RaycastHit? hit = GetRaycastHit();

        if (hit != null)
        {
            pos4 = hit.Value.point;
        }
        else
        {
            pos4 = pos1 + transform.forward * _stats.MaxDistance;
        }

        float distance = Vector3.Distance(pos1, pos4);
        if (distance > _stats.MaxDistance)
        {
            pos4 = pos1 + transform.forward * _stats.MaxDistance;
        }

        ElectricRayUtils.SetBezierAndScale(transform, pos1, pos4);

        if (hit != null && distance <= _stats.MaxDistance)
            Collide(hit.Value.collider);
    }

    protected override void OnImpact(Collider other, float multiplier = 1)
    {
        base.OnImpact(other);
        Debug.Log("Ola");
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
        _vfx.SetFloat("Thickness", 0.0f);
        Destroy(gameObject, 0.5f);
    }
}
