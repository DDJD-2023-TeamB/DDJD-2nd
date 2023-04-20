using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FireballComponent : ChargeProjectileComponent
{
    [SerializeField]
    private float _explosionRadius = 3.0f;

    private float _currentRadius = 0.0f;
    private float _maxRadius = 0.25f;

    private VisualEffect _fireballVFX;

    private const int PARTICLES = 128;

    protected override void OnImpact(Collider other)
    {
        //Raycast sphere
        RaycastHit[] hits = Physics.SphereCastAll(
            transform.position,
            _explosionRadius,
            Vector3.up,
            0.0f
        );
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject == _caster)
            {
                continue;
            }
            // https://docs.unity3d.com/ScriptReference/Physics.SphereCastAll.html
            //Notes: For colliders that overlap the sphere at the start of the sweep,
            //RaycastHit.normal is set opposite to the direction of the sweep, RaycastHit.distance is set to zero, and the zero vector gets returned in RaycastHit.point
            Vector3 point = hit.distance == 0 ? hit.collider.bounds.center : hit.point;
            Vector3 direction = (point - transform.position).normalized;

            Damage(hit.collider.gameObject, (int)GetDamage(), point, direction);
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(
                    GetDamage(),
                    transform.position,
                    _explosionRadius,
                    0.0f,
                    ForceMode.Impulse
                );
            }
        }
    }

    override protected void Awake()
    {
        base.Awake();
        _fireballVFX = GetComponentInChildren<VisualEffect>();
    }

    override public void SetSkill(Skill skill)
    {
        base.SetSkill(skill);
        _fireballVFX.SetFloat("SpawnRadius", 0.1f);
        _fireballVFX.SetFloat("SpawnRate", PARTICLES / _stats.MaxChargeTime);
    }

    protected override void OnCharge()
    {
        float previousRadius = _currentRadius;
        _currentRadius = Mathf.Lerp(0, _maxRadius, GetCurrentCharge());
        float radiusChange = _currentRadius - previousRadius;
        transform.position += _caster.transform.forward * radiusChange;
        _fireballVFX.SetFloat("Size", _currentRadius);
        _fireballVFX.SetFloat("SpawnRadius", _currentRadius * 4.0f);
    }

    public override void Shoot(Vector3 direction)
    {
        base.Shoot(direction);
        _explosionRadius = _explosionRadius * GetCurrentCharge();
        _fireballVFX.SetFloat("SpawnRate", 0);
    }

    protected override void SpawnHitVFX()
    {
        GameObject impact = Instantiate(_impactPrefab, transform.position, Quaternion.identity);
        VisualEffect vfx = impact.GetComponent<VisualEffect>();
        vfx.SetFloat("Size", _currentRadius);
        Destroy(impact, 3.0f);
    }

    private float GetDamage()
    {
        return _stats.Damage * GetCurrentCharge();
    }
}