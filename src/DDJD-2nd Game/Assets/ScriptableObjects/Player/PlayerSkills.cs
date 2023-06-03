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
    private Element _currentElement;
    public Element CurrentElement
    {
        get => _currentElement;
        set => _currentElement = value;
    }

    [SerializeField]
    private List<ItemSkill> _learnedSkills = new List<ItemSkill>();

    [SerializeField]
    private List<ItemSkill> _equippedLeftSkills = new List<ItemSkill>();

    [SerializeField]
    private List<ItemSkill> _equippedRightSkills = new List<ItemSkill>();

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

    [SerializeField]
    private DashStats _dashStats;
    public DashStats DashStats
    {
        get => _dashStats;
        set => _dashStats = value;
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
        _skillCooldowns[skill] = skill.SkillStats.Cooldown;
        _player.StartCoroutine(SkillCooldown(skill));
    }

    public IEnumerator SkillCooldown(Skill skill)
    {
        while (_skillCooldowns[skill] > 0)
        {
            _skillCooldowns[skill] -= COOLDOWN_TICK;
            yield return new WaitForSeconds(COOLDOWN_TICK);
        }
        if (_skillCooldowns.ContainsKey(skill))
            _skillCooldowns.Remove(skill);
    }

    public bool IsSkillOnCooldown(Skill skill)
    {
        return _skillCooldowns.ContainsKey(skill);
    }

    public List<ItemSkill> EquippedRightSkills
    {
        get => _equippedRightSkills;
        set => _equippedRightSkills = value;
    }

    public List<ItemSkill> EquippedLeftSkills
    {
        get => _equippedLeftSkills;
        set => _equippedLeftSkills = value;
    }

    public List<ItemSkill> LearnedSkills
    {
        get => _learnedSkills;
        set => _learnedSkills = value;
    }
}
