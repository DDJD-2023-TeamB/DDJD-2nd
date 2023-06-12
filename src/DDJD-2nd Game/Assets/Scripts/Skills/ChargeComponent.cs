using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChargeComponent : MonoBehaviour
{
    private float _chargeTime = 0.0f;
    private bool _isCharging = false;

    public Action OnChargeComplete;
    public Action OnCharge;

    private CharacterStatus _characterStatus;
    private int _maxManaCost = 0;

    private float _maxChargeTime = 0.0f;
    private float _minChargeTime = 0.0f;

    public float MaxChargeTime
    {
        get => _maxChargeTime;
        set => _maxChargeTime = value;
    }

    public float MinChargeTime
    {
        get => _minChargeTime;
        set => _minChargeTime = value;
    }

    void Awake()
    {
        _isCharging = true;
    }

    protected void Charge()
    {
        if (!_isCharging)
        {
            return;
        }
        _chargeTime += Time.deltaTime;
        if (_chargeTime <= _minChargeTime)
        {
            _chargeTime = _minChargeTime;
        }
        if (!_characterStatus.HasEnoughMana(GetManaCost()))
        {
            _isCharging = false;
            _chargeTime -= Time.deltaTime;
            return;
        }

        if (_chargeTime >= MaxChargeTime)
        {
            _chargeTime = MaxChargeTime;
        }
        OnCharge?.Invoke();
        if (_chargeTime >= MaxChargeTime)
        {
            OnChargeComplete?.Invoke();
            _isCharging = false;
        }
    }

    void Update()
    {
        Charge();
    }

    public float GetCurrentCharge()
    {
        return _chargeTime / MaxChargeTime;
    }

    public int GetManaCost()
    {
        return Mathf.CeilToInt(_maxManaCost * GetCurrentCharge());
    }

    public void StopCharging()
    {
        _isCharging = false;
    }

    public bool IsCharging()
    {
        return _isCharging;
    }

    public void SetCaster(GameObject caster)
    {
        _characterStatus = caster.GetComponent<CharacterStatus>();
    }

    public void SetManaCost(int manaCost)
    {
        _maxManaCost = manaCost;
    }
}
