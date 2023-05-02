using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BasicStoneThrowComponent : ProjectileComponent
{
    private VisualEffect _vfx;

    protected override void Awake()
    {
        base.Awake();
        _vfx = GetComponent<VisualEffect>();
    }

    public override void Shoot(Vector3 direction)
    {
        StartCoroutine(ShootAfterDelay(direction));
    }

    private IEnumerator ShootAfterDelay(Vector3 direction)
    {
        yield return new WaitForSeconds(_stats.CastTime);
        _vfx.SendEvent("Throw");
        base.Shoot(direction);
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
