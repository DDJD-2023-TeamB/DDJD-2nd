using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnSkillComponent : SkillComponent
{
    protected SpawnSkill _skill;

    public override void SetSkill(Skill skill)
    {
        base.SetSkill(skill);
        _skill = (SpawnSkill)skill;
    }

    // Called after adding the object to ObjectSpawner's list
    public abstract void Spawn();
}
