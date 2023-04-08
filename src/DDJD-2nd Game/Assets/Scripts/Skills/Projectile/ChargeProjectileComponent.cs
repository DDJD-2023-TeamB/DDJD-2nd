using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public abstract class ChargeProjectileComponent : ProjectileComponent
{

    [SerializeField]
    protected float _maxChargeTime = 2.0f;

    [SerializeField]
    protected float _chargeSpeed = 1.0f;

    protected float _chargeTime = 0.0f;
    private bool _isCharging = false;

    private Transform _casterOffset;


    override protected void Awake(){
        base.Awake();
        _isCharging = true;

        

    }


    protected void Charge(){
        if(!_isCharging){
            return;
        }
        _chargeTime += Time.deltaTime;
        if(_chargeTime >= _maxChargeTime){
            _chargeTime = _maxChargeTime;
        }
        OnCharge();
    }

    public override void Shoot(Vector3 direction){
        base.Shoot(direction);
        _isCharging = false;
    }
    protected abstract void OnCharge();

    
    protected void Update(){
        Charge();
    }

    protected float GetCurrentCharge(){
        return _chargeTime / _maxChargeTime;
    }
    
}
