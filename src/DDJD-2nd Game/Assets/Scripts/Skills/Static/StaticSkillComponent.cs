using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StaticSkillComponent : SkillComponent
{
    private Collider _collider;

    protected override void Awake()
    {
        base.Awake();
        _collider = GetComponent<Collider>();
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
        transform.parent = null; // Detach from caster
        _shootDirection = direction;
        StartCoroutine(DestroyAfterTime(_stats.Duration));
    }

    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        _collider.enabled = false;
        Destroy(gameObject, 1.0f);
    }
}
