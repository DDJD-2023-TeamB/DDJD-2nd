using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectionRune : RuneShooter
{
    private float _speed = 3.0f;

    private Vector3 _casterOffset;
    private Vector3? _targetPosition;

    private SkillComponent _skillToBlock;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void StartShoot(BossEnemy caster, Skill skill)
    {
        base.StartShoot(caster, skill);
        _casterOffset = transform.position - _caster.transform.position;
        _targetPosition = null;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Vector3 desiredPosition;
        if (_targetPosition != null)
        {
            desiredPosition = _targetPosition.GetValueOrDefault();
        }
        else
        {
            desiredPosition = _caster.transform.position + _casterOffset;
        }
        float distanceToCaster = Vector3.Distance(transform.position, desiredPosition);
        Vector3 direction = (desiredPosition - transform.position).normalized;
        _rigidbody.velocity = direction * _speed * distanceToCaster;

        if (_skillToBlock == null)
        {
            _skillToBlock = null;
            _targetPosition = null;
            return;
        }
        if ((_skillToBlock.transform.position - transform.position).magnitude <= 1.5f)
        {
            CreateSpell();
            _targetPosition = null;
            _skillToBlock = null;
        }
    }

    protected override IEnumerator Shoot()
    {
        yield return new WaitForSeconds(0.5f);
    }

    protected override void UpdateRotation()
    {
        if (_skillToBlock == null)
        {
            base.UpdateRotation();
        }
        else
        {
            Vector3 direction = (_skillToBlock.transform.position - transform.position).normalized;
            // slowly rotate towards the player
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(direction),
                0.1f
            );
        }
    }

    public void SetTargetSkill(SkillComponent skill)
    {
        Vector3 casterPostion = _caster.transform.position;
        casterPostion.y += 0.9f;
        Vector3 direction = (skill.transform.position - casterPostion).normalized;

        _skillToBlock = skill;
        _targetPosition = casterPostion + direction * 1.0f;
    }
}
