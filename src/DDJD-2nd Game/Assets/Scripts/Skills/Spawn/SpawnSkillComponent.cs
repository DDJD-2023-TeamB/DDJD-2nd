using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using FMODUnity;

public abstract class SpawnSkillComponent : SkillComponent
{
    protected SpawnSkill _spawnSkill;

    public override void SetSkill(Skill skill)
    {
        base.SetSkill(skill);
        _spawnSkill = (SpawnSkill)skill;
    }

    // Called after adding the object to ObjectSpawner's list
    public abstract void Spawn();
}
