using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoveringRune : RuneShooter
{
    private float _speed = 1.0f;

    private Vector3 _casterOffset;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void StartShoot(BossEnemy caster, Skill skill)
    {
        base.StartShoot(caster, skill);
        _casterOffset = transform.position - _caster.transform.position;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Vector3 desiredPosition = _caster.transform.position + _casterOffset;
        float distanceToCaster = Vector3.Distance(transform.position, desiredPosition);
        Vector3 direction = (desiredPosition - transform.position).normalized;
        _rigidbody.velocity = direction * _speed * distanceToCaster;
    }
}
