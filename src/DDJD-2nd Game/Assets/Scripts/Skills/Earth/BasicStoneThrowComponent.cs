using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BasicStoneThrowComponent : ProjectileComponent
{
    private VisualEffect _vfx;

    private FMOD.Studio.PARAMETER_ID _sfxStateId;

    protected override void Awake()
    {
        base.Awake();
        _vfx = GetComponent<VisualEffect>();
    }

    protected void Start()
    {
        _sfxStateId = _soundEmitter.GetParameterId("shot", "Basic Earth Shot State");
    }

    public override void Shoot(Vector3 direction)
    {
        transform.parent = null;
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

        _soundEmitter.SetParameterWithLabel("shot", _sfxStateId, "Impact", false);
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
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        DeactivateSpell();
        Destroy(gameObject, 2.0f);
    }
}
