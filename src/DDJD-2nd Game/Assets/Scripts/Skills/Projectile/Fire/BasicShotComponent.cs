using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BasicShotComponent : ProjectileComponent
{
    private VisualEffect _vfx;

    private SoundEmitter _soundEmitter;

    protected override void Awake()
    {
        base.Awake();
        _vfx = GetComponent<VisualEffect>();
        _soundEmitter = GetComponent<SoundEmitter>();
    }

    public override void Shoot(Vector3 direction)
    {
        base.Shoot(direction);
        _soundEmitter.PlayAndRelease("spawn");
    }

    protected override void OnImpact(Collider other, float multiplier = 1)
    {
        base.OnImpact(other);

        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(transform.forward * _skillStats.ForceWithDamage(), ForceMode.Impulse);
        }

        Damage(
            other.gameObject,
            (int)(_skillStats.Damage * multiplier),
            (int)_skillStats.ForceWithDamage(),
            other.ClosestPoint(_caster.transform.position),
            _caster.transform.forward
        );
    }

    public override void DestroySpell()
    {
        _vfx.Stop();
        _vfx.SetFloat("ProjectileSize", 0.0f);
        DeactivateSpell();
        Destroy(gameObject, 2.0f);
    }
}
