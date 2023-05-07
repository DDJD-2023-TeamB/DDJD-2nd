using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Tornado : GroundProjectileComponent, NonCollidable
{
    private float _currentSize = 0.0f;

    private float _maxSize = 0.3f;

    private VisualEffect _tornadoVFX;

    [Tooltip("The axis that the caught objects will rotate around")]
    private Vector3 _rotationAxis = new Vector3(0, 1, 0);
    public Vector3 RotationAxis
    {
        get => _rotationAxis;
    }

    [Tooltip("Angle that is added to the object's velocity (higher lift -> quicker on top)")]
    [Range(0, 90)]
    [SerializeField]
    private float _lift = 45.0f;
    public float Lift
    {
        get => _lift;
    }

    private float _tornadoStrength;
    public float TornadoStrength
    {
        get => _tornadoStrength;
    }

    [Tooltip("The force that will drive the caught objects around the tornado's center")]
    private float _rotationStrength = 50;
    public float RotationStrength
    {
        get => _rotationStrength;
    }

    private float _lifetime;

    private List<CaughtInTornado> _caughtObjects;

    override protected void Awake()
    {
        base.Awake();
        _lifetime = 0.0f;
        _caughtObjects = new List<CaughtInTornado>();
        _tornadoVFX = GetComponentInChildren<VisualEffect>();
        _chargeComponent.OnChargeComplete += StopGeneratingTornado;
        //StartCoroutine(SpawnTornado());
    }

    private IEnumerator SpawnTornado()
    {
        _tornadoVFX.SetFloat("Duration", 5.0f);
        yield return new WaitForSeconds(1.0f);
        _tornadoVFX.SetFloat("Duration", 100.0f);
    }

    public override void SetSkill(Skill skill)
    {
        base.SetSkill(skill);
        _chargeComponent.OnCharge += OnCharge;
        SetTornadoSize();
    }

    override protected void Update()
    {
        base.Update();
        _lifetime += Time.deltaTime;
    }

    public override void SetCaster(GameObject caster)
    {
        base.SetCaster(caster);
        //Set rotation to caster
        Vector3 newPosition = transform.position;
        newPosition.y = caster.transform.position.y + 0.2f;
        transform.position = newPosition;
        //transform.rotation = _caster.transform.rotation;
    }

    private void StopGeneratingTornado()
    {
        _tornadoVFX.SendEvent("StopGeneratingTornado");
    }

    public override void Shoot(Vector3 direction)
    {
        base.Shoot(direction);

        //Send event
        StopGeneratingTornado();
        //Change collider scale
        BoxCollider boxCollider = (BoxCollider)_collider;
        Vector3 originalSize = boxCollider.size;
        boxCollider.center = new Vector3(
            0,
            originalSize.y / 2 * _chargeComponent.GetCurrentCharge(),
            0
        );
        boxCollider.size = originalSize * _chargeComponent.GetCurrentCharge();
        _tornadoStrength = _skillStats.Force * _chargeComponent.GetCurrentCharge();
        _rotationStrength = 50f * _chargeComponent.GetCurrentCharge();

        StartCoroutine(DestroyTornado());
    }

    private IEnumerator DestroyTornado()
    {
        float timeToDestroy = _stats.Range / _stats.Speed;
        yield return new WaitForSeconds(timeToDestroy);

        _tornadoVFX.SetFloat("Duration", _lifetime + 2.0f);

        yield return new WaitForSeconds(1.5f);
        _collider.enabled = false;
        for (int i = 0; i < _caughtObjects.Count; i++)
        {
            if (_caughtObjects[i] != null)
            {
                _caughtObjects[i].Release();
            }
        }

        Destroy(gameObject, 2.0f);
    }

    protected override void OnImpact(Collider other, float multiplier)
    {
        if (!other.attachedRigidbody)
            return;
        if (other.attachedRigidbody.isKinematic)
            return;

        //Add caught object to the list
        CaughtInTornado caught = other.GetComponent<CaughtInTornado>();
        if (!caught)
        {
            caught = other.gameObject.AddComponent<CaughtInTornado>();
        }

        if (!_caughtObjects.Contains(caught))
        {
            caught.Init(this, _rb, _tornadoStrength);
            _caughtObjects.Add(caught);
        }

        //Damage
        Damageable damageable = other.GetComponent<Damageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(
                (int)(_stats.Damage * multiplier),
                _tornadoStrength,
                other.ClosestPoint(transform.position),
                transform.forward
            );
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (!other.attachedRigidbody)
            return;
        //Release caught object
        CaughtInTornado caught = other.GetComponent<CaughtInTornado>();
        if (caught)
        {
            caught.Release();

            if (_caughtObjects.Contains(caught))
            {
                _caughtObjects.Remove(caught);
            }
        }
    }

    private void OnCharge()
    {
        float previousSize = _currentSize;
        _currentSize = Mathf.Lerp(0, _maxSize, _chargeComponent.GetCurrentCharge());
        float sizeChange = _currentSize - previousSize;
        transform.position += _caster.transform.forward * sizeChange;
        SetTornadoSize();
    }

    private void SetTornadoSize()
    {
        _tornadoVFX.SetVector3(
            "Scale",
            new Vector3(_currentSize * 2.0f, _currentSize * 2.0f, _currentSize)
        );
    }
}
