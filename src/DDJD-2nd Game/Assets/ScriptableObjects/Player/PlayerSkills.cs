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
    private List<Element> _elements = new List<Element>();
    public List<Element> Elements
    {
        get => _elements;
        set => _elements = value;
    }

    [SerializeField]
    private List<ItemSkill> _learnedSkills = new List<ItemSkill>();

    [SerializeField]
    private ItemSkill[] _equippedLeftSkills = new ItemSkill[6];

    [SerializeField]
    private ItemSkill[] _equippedRightSkills = new ItemSkill[6];

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

    Dictionary<EquippedSkill, float> _skillCooldowns = new Dictionary<EquippedSkill, float>();

    private Player _player;
    public Player Player
    {
        set => _player = value;
    }

    private float COOLDOWN_TICK = 0.1f; //Seconds

    public void StartSkillCooldown(Skill skill, bool isLeft)
    {
        EquippedSkill equippedSkill = new EquippedSkill(skill, isLeft);
        if (!_skillCooldowns.ContainsKey(equippedSkill))
        {
            _skillCooldowns.Add(equippedSkill, skill.SkillStats.Cooldown);
        }
        _skillCooldowns[equippedSkill] = skill.SkillStats.Cooldown;
        _player.StartCoroutine(SkillCooldown(equippedSkill));
    }

    public IEnumerator SkillCooldown(EquippedSkill equippedSkill)
    {
        while (_skillCooldowns[equippedSkill] > 0)
        {
            _skillCooldowns[equippedSkill] -= COOLDOWN_TICK;
            yield return new WaitForSeconds(COOLDOWN_TICK);
        }
        if (_skillCooldowns.ContainsKey(equippedSkill))
            _skillCooldowns.Remove(equippedSkill);
    }

    public bool IsSkillOnCooldown(Skill skill, bool isLeft)
    {
        EquippedSkill equippedSkill = new EquippedSkill(skill, isLeft);
        return _skillCooldowns.ContainsKey(equippedSkill);
    }

    public ItemSkill[] EquippedRightSkills
    {
        get => _equippedRightSkills;
        set => _equippedRightSkills = value;
    }

    public ItemSkill[] EquippedLeftSkills
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

public class EquippedSkill
{
    Skill _skill;
    public Skill Skill
    {
        get => _skill;
    }
    private bool _isLeft;
    public bool IsLeft
    {
        get => _isLeft;
    }

    public EquippedSkill(Skill skill, bool isLeft)
    {
        _skill = skill;
        _isLeft = isLeft;
    }

    public override int GetHashCode()
    {
        return _skill.GetHashCode() + (_isLeft ? 1 : 0);
    }

    public override bool Equals(object obj)
    {
        if (obj is EquippedSkill equippedSkill)
        {
            return equippedSkill.Skill == _skill && equippedSkill.IsLeft == _isLeft;
        }
        return false;
    }
}
