using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillComponent : MonoBehaviour
{
    protected GameObject _caster;

    public void SetCaster(GameObject caster)
    {
        _caster = caster;
    }

    public abstract void SetSkill(Skill skill);
}
