using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StaticSkillComponent : SkillComponent
{
    [SerializeField]
    private bool _leaveCaster = true;
    protected Collider _collider;

    protected override void Awake()
    {
        base.Awake();
        _collider = GetComponent<Collider>();
    }

    protected void DeactivateSpell()
    {
        if (_collider != null)
        {
            _collider.enabled = false;
        }
    }

    protected StaticSkillStats _stats;
    protected StaticSkill _skill;

    protected Vector3 _shootDirection;

    public override void SetSkill(Skill skill)
    {
        base.SetSkill(skill);
        _skill = (StaticSkill)skill;
        _stats = _skill.StaticStats;
    }

    public override void Shoot(Vector3 direction)
    {
        base.Shoot(direction);
        _shootDirection = direction;
        if (_leaveCaster)
        {
            transform.parent = null; // Detach from caster
        }
        if (_stats.Duration >= 0.0f)
        {
            StartCoroutine(DestroyAfterTime(_stats.Duration));
        }
    }

    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        DeactivateSpell();
        DestroySpell();
    }
}
