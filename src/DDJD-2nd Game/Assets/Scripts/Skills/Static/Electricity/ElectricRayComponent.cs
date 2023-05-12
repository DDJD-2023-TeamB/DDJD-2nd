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

    private void Start()
    {
        SetRayPosition();
    }

    public override void Shoot(Vector3 direction)
    {
        base.Shoot(direction);
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
        DeactivateSpell();
        _vfx.Stop();
        Destroy(gameObject, 3.0f);
    }

    private void SetRayPosition()
    {
        Vector3? hitPosition = GetHitPosition();
        if (hitPosition == null)
        {
            DestroySpell();
            return;
        }
        // TODO extract this somewhere and use it in ElectricArcCoilComponent
        // Calculate Bezier arc
        Vector3 pos1 = transform.position;
        Vector3 pos4 = hitPosition.Value;
        Vector3 pos2 = 2 * pos1 / 3 + pos4 / 3;
        Vector3 pos3 = pos1 / 3 + 2 * pos4 / 3;

        // Set position and rotation
        transform.position = (pos1 + pos4) / 2;
        transform.right = pos1 - pos4;

        // Get bezier points
        Transform arcPos1 = transform.Find("Pos1");
        Transform arcPos2 = transform.Find("Pos2");
        Transform arcPos3 = transform.Find("Pos3");
        Transform arcPos4 = transform.Find("Pos4");

        // Scale object for correct box collider
        float realDistance = Vector3.Distance(pos1, pos4);
        float bezierDistance = Vector3.Distance(arcPos1.position, arcPos4.position);
        float scale = realDistance / bezierDistance;
        transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);

        // Set VFX positions
        arcPos1.position = pos1;
        arcPos2.position = pos2;
        arcPos3.position = pos3;
        arcPos4.position = pos4;
    }

    private Vector3? GetHitPosition()
    {
        AimComponent aimComponent = _caster.GetComponent<AimComponent>();
        RaycastHit hit;
        if (aimComponent.GetAimRaycastHit(out hit))
        {
            return hit.point;
        }
        return null;
    }
}
