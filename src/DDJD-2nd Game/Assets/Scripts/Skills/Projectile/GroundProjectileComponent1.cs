using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GroundProjectileComponent : ProjectileComponent
{
    [Tooltip("The offset of the projectile from the caster's position")]
    [SerializeField]
    protected Vector3 _offset = new Vector3(0.0f, 0.5f, 2.0f);

    private Transform _casterTransform;

    public override void SetCaster(GameObject caster)
    {
        base.SetCaster(caster);
        _casterTransform = _caster.transform;
        transform.parent = null;
    }

    protected override void Update()
    {
        base.Update();
        if (!_leftCaster)
        {
            transform.position =
                _casterTransform.position
                + _offset.z * _casterTransform.forward
                + _offset.y * _casterTransform.up;
            transform.rotation = _casterTransform.rotation;
        }
    }

    public override void Shoot(Vector3 direction)
    {
        direction.y = 0.0f;
        base.Shoot(direction);
        transform.rotation = _caster.transform.rotation;
    }
}
