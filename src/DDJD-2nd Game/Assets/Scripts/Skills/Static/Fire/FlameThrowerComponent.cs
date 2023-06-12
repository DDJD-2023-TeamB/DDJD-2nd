using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using FMODUnity;

public class FlameThrowerComponent : StaticSkillComponent, NonCollidable
{
    private VisualEffect _vfx;
    private FMOD.Studio.PARAMETER_ID _sfxStateId;

    private Coroutine _soundRoutine;

    protected override void Awake()
    {
        base.Awake();
        _vfx = GetComponent<VisualEffect>();
    }

    private string _sfxName = "flamethrower";

    protected void Start()
    {
        _sfxStateId = _soundEmitter.GetParameterId(_sfxName, "Flame Thrower State");
        _soundRoutine = _soundEmitter.CallWithDelay(
            () => _soundEmitter.SetParameterWithLabel(_sfxName, _sfxStateId, "On", false),
            0.05f
        );
    }

    public override void Shoot(Vector3 direction)
    {
        base.Shoot(direction);

        //face the direction of the caster
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
        _soundEmitter.Stop(_sfxName);
        _soundEmitter.SetParameterWithLabel(_sfxName, _sfxStateId, "Off", true);
        _soundEmitter.CallWithDelay(() => _soundEmitter.StopAndRelease(_sfxName), 1.0f);
        _vfx.Stop();
        if (_soundRoutine != null)
        {
            StopCoroutine(_soundRoutine);
        }

        active = false;
        Destroy(gameObject, 3.0f);
    }

    public void OnDestroy()
    {
        _soundEmitter.StopAndReleaseAll();
    }
}
