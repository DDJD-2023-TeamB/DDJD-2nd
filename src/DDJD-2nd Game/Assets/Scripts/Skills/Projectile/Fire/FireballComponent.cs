using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FireballComponent : ProjectileComponent, NonPushable
{
    [SerializeField]
    private float _explosionRadius = 3.0f;

    private float _currentRadius = 0.0f;
    private float _maxRadius = 0.25f;

    private VisualEffect _fireballVFX;

    private const int PARTICLES = 128;

    private FMOD.Studio.PARAMETER_ID _sfxChargeId;
    private FMOD.Studio.PARAMETER_ID _sfxStateId;
    private FMOD.Studio.PARAMETER_ID _sfxExplosionChargeId;

    private bool _exploded = false;

    protected override void Awake()
    {
        base.Awake();
        _fireballVFX = GetComponentInChildren<VisualEffect>();
    }

    protected void Start()
    {
        _sfxChargeId = _soundEmitter.GetParameterId("fireball", "Charged Fire Ball Intensity");
        _sfxStateId = _soundEmitter.GetParameterId("fireball", "Ball State");
        _sfxExplosionChargeId = _soundEmitter.GetParameterId(
            "explosion",
            "Charged Fire Ball Intensity"
        );
        _soundEmitter.SetParameter("fireball", _sfxChargeId, 0.0f);

        _chargeComponent.OnChargeComplete += OnChargeComplete;
    }

    protected override void OnImpact(Collider other, float multiplier = 1)
    {
        base.OnImpact(other);
    }

    override public void SetSkill(Skill skill)
    {
        base.SetSkill(skill);
        _fireballVFX.SetFloat("SpawnRadius", 0.1f);
        _fireballVFX.SetFloat("SpawnRate", PARTICLES / _stats.MaxChargeTime);

        _chargeComponent.OnCharge += OnCharge;
    }

    protected void OnCharge()
    {
        float previousRadius = _currentRadius;
        _currentRadius = Mathf.Lerp(0, _maxRadius, _chargeComponent.GetCurrentCharge());
        float radiusChange = _currentRadius - previousRadius;
        transform.position += _caster.transform.forward * radiusChange;
        _fireballVFX.SetFloat("Size", _currentRadius);
        _fireballVFX.SetFloat("SpawnRadius", _currentRadius * 4.0f);
    }

    private void OnChargeComplete()
    {
        //_soundEmitter.SetParameterWithLabel("fireball", _sfxStateId, "Iddle", false);
    }

    public override void Shoot(Vector3 direction)
    {
        base.Shoot(direction);
        _soundEmitter.SetParameter("fireball", _sfxChargeId, _chargeComponent.GetCurrentCharge());
        _soundEmitter.SetParameter("fireball", _sfxStateId, 1);
        _soundEmitter.Play("idle");
        _explosionRadius = _explosionRadius * _chargeComponent.GetCurrentCharge();
        _fireballVFX.SetFloat("SpawnRate", 0);
    }

    protected override void SpawnHitVFX()
    {
        GameObject impact = Instantiate(_impactPrefab, transform.position, Quaternion.identity);
        VisualEffect vfx = impact.GetComponent<VisualEffect>();
        vfx.SetFloat("Size", _currentRadius);
        _soundEmitter.SetParameter(
            "explosion",
            _sfxExplosionChargeId,
            _chargeComponent.GetCurrentCharge()
        );
        _soundEmitter.StopAndRelease("idle");
        _soundEmitter.UpdatePosition("explosion");
        _soundEmitter.Play("explosion");
        Destroy(impact, 3.0f);
    }

    public override void DestroySpell()
    {
        DeactivateSpell();
        _soundEmitter.StopAndRelease("fireball");
        Destroy(gameObject);
    }

    protected override void Explode()
    {
        DeactivateSpell();
        SpawnHitVFX();
        _soundEmitter.Stop("fireball");
        _fireballVFX.enabled = false;

        //Raycast sphere
        RaycastHit[] hits = Physics.SphereCastAll(
            transform.position,
            _explosionRadius,
            Vector3.up,
            0.0f
        );
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject == _caster || hit.collider.gameObject == gameObject)
            {
                continue;
            }
            // https://docs.unity3d.com/ScriptReference/Physics.SphereCastAll.html
            //Notes: For colliders that overlap the sphere at the start of the sweep,
            //RaycastHit.normal is set opposite to the direction of the sweep, RaycastHit.distance is set to zero, and the zero vector gets returned in RaycastHit.point
            Vector3 point = hit.distance == 0 ? hit.collider.bounds.center : hit.point;
            Vector3 direction = (point - transform.position).normalized;
            float force = GetForce();
            Damage(hit.collider.gameObject, (int)GetDamage(), (int)force, point, direction);
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null && hit.collider.GetComponent<NonPushable>() == null)
            {
                rb.AddExplosionForce(
                    force,
                    transform.position,
                    _explosionRadius,
                    0.0f,
                    ForceMode.Impulse
                );
            }
        }

        StartCoroutine(DestroyAfterDelay(2.0f));
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        DestroySpell();
    }
}
