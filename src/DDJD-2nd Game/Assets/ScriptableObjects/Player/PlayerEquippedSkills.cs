using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerEquippedSkills", menuName = "ScriptableObjects/OnlyOnce/Player/PlayerEquippedSkills", order = 1)]
public class PlayerEquippedSkills : ScriptableObject
{
    [SerializeField]
    private Skill _leftSkill;

    [SerializeField]
    private Skill _rightSkill;

    public Skill LeftSkill { get => _leftSkill; set => _leftSkill = value; }
    public Skill RightSkill { get => _rightSkill; set => _rightSkill = value; }
}
