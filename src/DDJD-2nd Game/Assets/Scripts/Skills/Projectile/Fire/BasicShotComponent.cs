using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BasicShotComponent : ProjectileComponent, NonPushable
{
    private VisualEffect _vfx;

    [SerializeField]
    private string _sfxStateName;

    private FMOD.Studio.PARAMETER_ID _sfxStateId;

    protected override void Awake()
    {
        base.Awake();
        _vfx = GetComponent<VisualEffect>();
    }

    protected void Start()
    {
        _sfxStateId = _soundEmitter.GetParameterId("shot", _sfxStateName);
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
        if (rb != null && other.GetComponent<NonPushable>() == null)
        {
            rb.AddForce(transform.forward * _skillStats.ForceWithDamage(), ForceMode.Impulse);
        }

        _soundEmitter.UpdatePosition("shot");
        _soundEmitter.SetParameterWithLabel("shot", _sfxStateId, "Impact", false);

        Damage(
            other.gameObject,
            (int)(_skillStats.Damage * multiplier),
            (int)_skillStats.ForceWithDamage(),
            transform.position,
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
