using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
using System;

public class CharacterStatus : MonoBehaviour
{
    [SerializeField]
    protected int _health;

    [SerializeField]
    protected int _maxHealth = 100;

    [SerializeField]
    protected bool _useMana = false;
    public bool UseMana
    {
        get { return _useMana; }
    }

    [SerializeField]
    [ConditionalField(nameof(_useMana), false, true)]
    protected int _mana;
    public int Mana
    {
        get { return _mana; }
        set { _mana = value; }
    }

    [SerializeField]
    [ConditionalField(nameof(_useMana), false, true)]
    protected int _maxMana = 100;

    protected Action<int, Vector3, Vector3> _onDeath;
    public Action<int, Vector3, Vector3> OnDeath
    {
        get { return _onDeath; }
        set { _onDeath = value; }
    }

    void Awake()
    {
        _health = _maxHealth;
        _mana = _maxMana;
    }

    // Update is called once per frame
    void Update() { }

    public void TakeDamage(
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

    public bool HasEnoughMana(int manaCost)
    {
        return _mana >= manaCost;
    }

    public bool ConsumeMana(int manaCost)
    {
        if (_mana < manaCost)
        {
            return false;
        }
        _mana -= manaCost;
        return true;
    }

    public void RestoreMana(int manaQuantity)
    {
        _mana = Mathf.Min(_mana + manaQuantity, _maxMana);
    }
}
