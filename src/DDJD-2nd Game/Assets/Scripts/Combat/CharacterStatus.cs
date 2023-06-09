using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterStatus : MonoBehaviour
{

    [SerializeField]
    protected int _health;

    [SerializeField]
    protected int _maxHealth = 100;

    protected Action<int, Vector3, Vector3> _onDeath;
    public Action<int, Vector3, Vector3> OnDeath
    {
        get { return _onDeath; }
        set { _onDeath = value; }
    }

    protected virtual void Awake()
    {
        _health = _maxHealth;
    }

    // Update is called once per frame
    void Update() { }

    public virtual void TakeDamage(
        GameObject damager,
        int damage,
        Vector3 hitPoint = default(Vector3),
        Vector3 hitDirection = default(Vector3)
    )
    {
        _health -= damage;
        if (_health <= 0)
        {
            _onDeath?.Invoke(damage, hitPoint, hitDirection);
        }
    }
}
