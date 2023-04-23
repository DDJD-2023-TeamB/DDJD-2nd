using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GroundProjectileComponent : ProjectileComponent
{
    public override void Shoot(Vector3 direction)
    {
        direction.y = 0.0f;
        base.Shoot(direction);
        transform.rotation = _caster.transform.rotation;
        Vector3 newPosition = transform.position;
        newPosition.y = _caster.transform.position.y + 0.2f;
        transform.position = newPosition;
    }
}
