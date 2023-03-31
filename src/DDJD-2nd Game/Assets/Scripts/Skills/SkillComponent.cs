using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SkillComponent
{
    void Shoot(Vector3 direction);

    void SetSkill(Skill skill);
}
