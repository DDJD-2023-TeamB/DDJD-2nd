using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, Damageable
{
    protected Rigidbody _rb;
    protected CharacterStatus _status;

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
    }

    // Update is called once per frame
    virtual public void Update() { }

    public virtual void TakeDamage(int damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        _status.TakeDamage(damage, hitPoint, hitDirection);
    }
}
