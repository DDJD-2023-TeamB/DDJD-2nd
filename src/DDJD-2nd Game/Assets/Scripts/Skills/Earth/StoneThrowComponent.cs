using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Unity.Mathematics;

public class StoneThrowComponent : ProjectileComponent, NonPushable
{
    private VisualEffect _vfx;
    private FMOD.Studio.PARAMETER_ID _sfxStateId;

    [SerializeField, Tooltip("Time after collision to destroy the projectile")]
    private float _destroyAfterCollision = 3.0f;

    [SerializeField, Tooltip("Minimum velocity to deal damage")]
    private float _velocityThreshold = 3.0f;

    private bool _canReboundSFX = true;

    protected override void Awake()
    {
        base.Awake();
        _vfx = GetComponent<VisualEffect>();
    }

    public void Start()
    {
        _sfxStateId = _soundEmitter.GetParameterId("stone", "Earth Shot State");
    }

    protected override void OnImpact(Collider other, float multiplier = 1)
    {
        if (_damagedObjects.Count > 0 && _rb.velocity.magnitude < _velocityThreshold)
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
        _soundEmitter.SetParameterWithLabel("stone", _sfxStateId, "Impact", true);
        StartCoroutine(DestroyAfterTime(_destroyAfterCollision));
    }

    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }

    public override void OnCollisionStay(Collision collision)
    {
        base.OnCollisionStay(collision);
        if (
            collision.gameObject.layer != LayerMask.NameToLayer("Enemy")
            && collision.gameObject.layer != LayerMask.NameToLayer("Player")
        )
        {
            if (_canReboundSFX)
            {
                _canReboundSFX = false;
                _soundEmitter.SetParameterWithLabel("stone", _sfxStateId, "Rebound", true);
                StartCoroutine(CanReboundRoutine());
            }
        }
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
            new Vector3(
                UnityEngine.Random.Range(-1, 1),
                UnityEngine.Random.Range(-1, 1),
                UnityEngine.Random.Range(-1, 1)
            ) * 100
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

    private IEnumerator CanReboundRoutine()
    {
        float velocity = _rb.velocity.magnitude;
        float duration = math.remap(1, 35.0f, 1.0f, 0.3f, velocity);
        yield return new WaitForSeconds(UnityEngine.Random.Range(duration, duration + 0.1f));
        _canReboundSFX = true;
    }
}
