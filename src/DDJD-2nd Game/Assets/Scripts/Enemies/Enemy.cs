using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : StateContext, Damageable
{
    protected Rigidbody _rb;
    protected CharacterStatus _status;

    [SerializeField]
    protected GameObject _deathVFX;

    private GenericState _currentState;

    virtual public void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _status = GetComponent<CharacterStatus>();
    }

    virtual public void Start()
    {
        _status.OnDeath += Die;
    }

    public virtual void Die(int force, Vector3 hitPoint, Vector3 hitDirection)
    {
        Destroy(gameObject);
        SpawnDeathVFX(transform.position);
    }

    protected GameObject SpawnDeathVFX(Vector3 position)
    {
        GameObject deathVFX = Instantiate(_deathVFX, position, transform.rotation);
        Destroy(gameObject, 0.1f);
        Destroy(deathVFX, 3f);
        return deathVFX;
    }

    // Update is called once per frame
    virtual public void Update() { }

    public virtual void TakeDamage(
        GameObject damager,
        int damage,
        float force,
        Vector3 hitPoint,
        Vector3 hitDirection
    )
    {
        _status.TakeDamage(damager, damage, hitPoint, hitDirection);
    }

    public virtual bool IsTriggerDamage()
    {
        return false;
    }

    public Rigidbody Rigidbody
    {
        get { return _rb; }
    }
}
