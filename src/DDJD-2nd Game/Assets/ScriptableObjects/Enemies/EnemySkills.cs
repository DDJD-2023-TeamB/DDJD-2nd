using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TypeReferences;

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
    private int _health = 100;

    [SerializeField]
    private float _maxAttacksInRow = 3;

    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    [Inherits(typeof(EnemyAttackState))]
    private TypeReference _attackStrategy;

    [SerializeField]
    [Inherits(typeof(EnemyChaseState))]
    private TypeReference _chaseStrategy;

    [SerializeField]
    [Inherits(typeof(EnemyIdleState))]
    private TypeReference _idleStrategy;

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

    public Type AttackStrategy
    {
        get => _attackStrategy;
    }

    public Type ChaseStrategy
    {
        get => _chaseStrategy;
    }

    public Type IdleStrategy
    {
        get => _idleStrategy;
    }

    public int Health
    {
        get => _health;
    }

    public float MaxAttacksInRow
    {
        get => _maxAttacksInRow;
    }

    public float Speed
    {
        get => _speed;
    }
}
