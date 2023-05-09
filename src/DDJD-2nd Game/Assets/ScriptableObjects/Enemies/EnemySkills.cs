using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "EnemySkills",
    menuName = "Scriptable Objects/Enemies/Skills",
    order = 1
)]
public class EnemySkills : ScriptableObject
{
    [SerializeField]
    private AimedSkill _leftSkill;

    [SerializeField]
    private AimedSkill _rightSkill;

    [SerializeField]
    private float _attackSpeed;

    [SerializeField]
    private bool _usesDash;
    public bool UsesDash
    {
        get => _usesDash;
    }

    [SerializeField]
    private Element _currentElement;
    public Element CurrentElement
    {
        get => _currentElement;
        set => _currentElement = value;
    }

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

    public float AttackSpeed
    {
        get => _attackSpeed;
    }
}
