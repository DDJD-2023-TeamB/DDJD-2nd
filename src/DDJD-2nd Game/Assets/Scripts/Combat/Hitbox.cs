using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Hitbox : MonoBehaviour, NonCollidable
{
    private GameObject _parent;
    private SoundEmitter _soundEmitter;

    private FMOD.Studio.PARAMETER_ID _hitsfxId;
    private FMOD.Studio.PARAMETER_ID _elementsfxId;
    public GameObject Parent
    {
        get { return _parent; }
        set { _parent = value; }
    }

    private GameObject _hitVfx;
    public GameObject HitVfx
    {
        get { return _hitVfx; }
        set { _hitVfx = value; }
    }
    private GameObject _attackVfx;

    private float _force;
    private float _damage;

    private Element _currentElement;

    private Dictionary<GameObject, bool> _alreadyHit;

    protected void Awake()
    {
        _soundEmitter = GetComponent<SoundEmitter>();
    }

    protected void Start()
    {
        _hitsfxId = _soundEmitter.GetParameterId("melee", "Basic Melee");
        _elementsfxId = _soundEmitter.GetParameterId("meleeElement", "Melee Type");
    }

    public void Activate(Element element, float force, float damage)
    {
        gameObject.SetActive(true);
        if (element?.AttackVfx != null)
        {
            _attackVfx = Instantiate(element.AttackVfx, transform.position, transform.rotation);
            _attackVfx.transform.parent = transform;
            _attackVfx
                .GetComponent<VisualEffect>()
                .SetVector3("Forward", _parent.transform.forward);
            _attackVfx.GetComponent<VisualEffect>().SetVector3("Up", _parent.transform.up);
        }
        _hitVfx = element.HitVfx;
        _currentElement = element;
        _soundEmitter?.SetParameterWithLabel("melee", _hitsfxId, "Miss", true);
        _soundEmitter?.UpdatePosition("melee");
        _soundEmitter?.SetParameterWithLabel(
            "meleeElement",
            _elementsfxId,
            element.SfxDamageLabel,
            true
        );
        _soundEmitter?.UpdatePosition("meleeElement");

        _force = force;
        _damage = damage;
        _alreadyHit = new Dictionary<GameObject, bool>();
    }

    public void Deactivate()
    {
        if (_attackVfx != null)
        {
            _attackVfx.GetComponent<VisualEffect>().Stop();
            _attackVfx.transform.parent = null;
            Destroy(_attackVfx, 2f);
        }
        gameObject.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _parent)
            return;

        if (other.gameObject.GetComponent<NonCollidable>() != null)
        {
            return;
        }
        if (_alreadyHit.Count > 0)
        {
            // If we decide to hit multiple objects, we need to check if the object is already hit
            // For now, can only hit one object. Hitting multiple objects was not working well with ragdolls
            return;
        }

        bool hitted = false;
        hitted = Push(other.gameObject) || hitted;
        hitted = Damage(other.gameObject) || hitted;

        if (hitted)
        {
            _soundEmitter?.SetParameterWithLabel("melee", _hitsfxId, "Hit", false);
            SpawnHitVFX(transform.position);
            _alreadyHit.Add(other.gameObject, true);
        }
    }

    private void SpawnHitVFX(Vector3 position)
    {
        if (_hitVfx == null)
            return;
        GameObject hitVFX = Instantiate(_hitVfx, position, transform.rotation);
        Destroy(hitVFX, 3f);
    }

    private bool Push(GameObject other)
    {
        Rigidbody rigidbody = other.GetComponent<Rigidbody>();
        if (rigidbody == null)
            return false;
        rigidbody.AddForce(_parent.transform.forward * _force, ForceMode.Impulse);
        return true;
    }

    private bool Damage(GameObject other)
    {
        Damageable damageable = other.GetComponent<Damageable>();
        if (damageable == null)
            return false;
        damageable.TakeDamage(
            this.gameObject,
            (int)_damage,
            _force,
            transform.position,
            _parent.transform.forward,
            _currentElement
        );
        return true;
    }
}
