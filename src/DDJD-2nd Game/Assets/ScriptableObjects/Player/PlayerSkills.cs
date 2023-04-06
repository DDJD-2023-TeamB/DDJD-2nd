using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "PlayerSkills",
    menuName = "Scriptable Objects/OnlyOnce/Player/PlayerSkills",
    order = 1
)]
public class PlayerSkills : ScriptableObject
{
    [SerializeField]
    private AimedSkill _leftSkill;

    [SerializeField]
    private AimedSkill _rightSkill;

    [SerializeField]
    private Dash _dashSkill;

    public AimedSkill LeftSkill
    {
        get => _leftSkill;
        set => _leftSkill = value;
    }
    public AimedSkill RightSkill
    {
        get => _rightSkill;
        set => _rightSkill = value;
    }
    public Dash DashSkill
    {
        get => _dashSkill;
        set => _dashSkill = value;
    }

    Dictionary<Skill, float> _skillCooldowns = new Dictionary<Skill, float>();

    private Player _player;
    public Player Player
    {
        set => _player = value;
    }

    private float COOLDOWN_TICK = 0.1f; //Seconds

    public void StartSkillCooldown(Skill skill)
    {
        if (!_skillCooldowns.ContainsKey(skill))
        {
            _skillCooldowns.Add(skill, 0f);
        }
        Debug.Log("Starting cooldown for " + skill.Stats);
        _skillCooldowns[skill] = skill.Stats.Cooldown;
        _player.StartCoroutine(SkillCooldown(skill));
    }

    public IEnumerator SkillCooldown(Skill skill)
    {
        while (_skillCooldowns[skill] > 0)
        {
            _skillCooldowns[skill] -= COOLDOWN_TICK;
            yield return new WaitForSeconds(COOLDOWN_TICK);
        }
        _skillCooldowns.Remove(skill);
    }

    public bool IsSkillOnCooldown(Skill skill)
    {
        return _skillCooldowns.ContainsKey(skill);
    }
}
