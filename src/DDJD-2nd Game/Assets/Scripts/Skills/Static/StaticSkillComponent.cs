using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StaticSkillComponent : SkillComponent
{
    protected virtual void Awake() { }

    protected StaticSkillStats _stats;
    protected StaticSkill _skill;

    public override void SetSkill(Skill skill)
    {
        _skill = (StaticSkill)skill;
        _stats = _skill.StaticStats;
    }

    public override void Shoot(Vector3 direction)
    {
        transform.parent = null; // Detach from caster
        StartCoroutine(DestroyAfterTime(_stats.Duration));
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _caster)
        {
            return;
        }
        OnImpact(other);
    }

    protected virtual void OnImpact(Collider other)
    {
        // Override this method to add functionality
    }

    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject, 0.1f);
    }
}
