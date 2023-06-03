using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class StoneThrowComponent : ProjectileComponent
{
    private VisualEffect _vfx;
    private FMOD.Studio.PARAMETER_ID _sfxStateId;

    [SerializeField, Tooltip("Time after collision to destroy the projectile")]
    private float _destroyAfterCollision = 3.0f;

    [SerializeField, Tooltip("Minimum velocity to deal damage")]
    private float _velocityThreshold = 3.0f;

    protected override void Awake()
    {
        base.Awake();
        _vfx = GetComponent<VisualEffect>();
    }

    public void Start()
    {
        _sfxStateId = _soundEmitter.GetParameterId("stone", "state");
    }

    protected override void OnImpact(Collider other, float multiplier = 1)
    {
        if (_rb.velocity.magnitude < _velocityThreshold)
        {
            return;
        }
        base.OnImpact(other);
        Damage(
            other.gameObject,
            (int)(_skillStats.Damage * multiplier),
            (int)(_skillStats.ForceWithDamage() * multiplier),
            other.ClosestPoint(_caster.transform.position),
            _caster.transform.forward
        );
        StartCoroutine(DestroyAfterTime(_destroyAfterCollision));
    }

    public override void Shoot(Vector3 direction)
    {
        transform.parent = null; // Detach from caster
        transform.localRotation = Quaternion.LookRotation(direction);
        StartCoroutine(ShootDelay(_skillStats.CastTime + 0.2f, direction));
    }

    private IEnumerator ShootDelay(float delay, Vector3 direction)
    {
        yield return new WaitForSeconds(delay);
        _vfx.SendEvent("Throw");
        _rb.useGravity = true;
        base.Shoot(direction);
        //add torque
        _rb.AddTorque(
            new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1)) * 100
        );
    }

    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        DestroySpell();
    }

    public override void DestroySpell()
    {
        _vfx.Stop();
        _soundEmitter.SetParameterWithLabel("stone", _sfxStateId, "destroy", true);
        //raycast to find the ground
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 100.0f))
        {
            transform.position = hit.point;
        }

        Destroy(gameObject);
        SpawnHitVFX();
    }
}
