using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class WindDashComponent : DashComponent
{
    private VisualEffect _dashVFX;

    protected override void Awake()
    {
        base.Awake();
        _dashVFX = GetComponent<VisualEffect>();
    }

    public override void SetSkill(Skill skill)
    {
        base.SetSkill(skill);
        _dashVFX.SetFloat("Duration", _skill.DashStats.Duration + 0.2f);
        StartCoroutine(StopVFX());
    }

    private IEnumerator StopVFX()
    {
        yield return new WaitForSeconds(_skill.DashStats.Duration + 0.2f);
        _dashVFX.Stop();
        Destroy(gameObject, 1.0f);
    }

    protected override void Update()
    {
        base.Update();
        transform.position = _caster.transform.position;
    }

    public override void SetCaster(GameObject caster)
    {
        base.SetCaster(caster);
        transform.parent = caster.transform;
        transform.parent = caster.transform.parent;
    }
}
