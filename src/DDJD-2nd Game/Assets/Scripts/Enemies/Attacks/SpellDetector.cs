using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDetector : MonoBehaviour, NonCollidable
{
    [SerializeField]
    private ProtectionRune _rune;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Attack"))
        {
            SkillComponent skill = other.GetComponent<SkillComponent>();
            if (skill == null)
            {
                return;
            }
            if (skill.Caster.layer == LayerMask.NameToLayer("Player"))
            {
                _rune.SetTargetSkill(skill);
            }
        }
    }
}
