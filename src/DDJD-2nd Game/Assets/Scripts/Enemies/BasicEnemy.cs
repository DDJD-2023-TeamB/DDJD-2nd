using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class BasicEnemy : HumanoidEnemy
{
    private Player _player;
    private NavMeshAgent _navMeshAgent;

    [Header("Ranges")]
    [SerializeField]
    private float _attackRange = 10.0f;

    [SerializeField]
    private float _aggroRange = 40.0f;

    protected EnemyStates _states;

    private int _forwardSpeedHash = Animator.StringToHash("ForwardSpeed");
    private int _rightSpeedHash = Animator.StringToHash("RightSpeed");

    [SerializeField]
    private EnemySkills _enemySkills;

    [SerializeField]
    [Tooltip("The amount of force applied to the enemy required to knock it down")]
    private float _forceResistance = 20f;

    private EnemyLineOfSight _lineOfSight;

    public override void Awake()
    {
        base.Awake();
        _lineOfSight = GetComponent<EnemyLineOfSight>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _states = new EnemyStates(
            chaseState: new EnemyChaseState(this),
            attackState: new EnemyAttackState(this),
            idleState: new EnemyIdleState(this)
        );
    }

    public override void Start()
    {
        base.Start();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        ChangeState(_states.IdleState);
    }

    public override void Die(int force, Vector3 hitPoint, Vector3 hitDirection)
    {
        base.Die(force, hitPoint, hitDirection);
        ChangeState(new EnemyDeadState(this));
    }

    public override void Update()
    {
        base.Update();
        _state.StateUpdate();
    }

    public override void TakeDamage(int damage, float force, Vector3 hitPoint, Vector3 hitDirection)
    {
        base.TakeDamage(damage, force, hitPoint, hitDirection);

        if (force >= _forceResistance)
        {
            ChangeState(new EnemyKnockdownState(this, force, hitPoint, hitDirection));
        }
    }

    //getters and setters
    public Player Player
    {
        get { return _player; }
    }
    public NavMeshAgent NavMeshAgent
    {
        get { return _navMeshAgent; }
    }

    public float AttackRange
    {
        get { return _attackRange; }
    }
    public float AggroRange
    {
        get { return _aggroRange; }
    }

    public int ForwardSpeedHash
    {
        get { return _forwardSpeedHash; }
    }

    public int RightSpeedHash
    {
        get { return _rightSpeedHash; }
    }

    public EnemyStates States
    {
        get { return _states; }
    }

    public EnemySkills EnemySkills
    {
        get { return _enemySkills; }
    }

    public EnemyLineOfSight LineOfSight
    {
        get { return _lineOfSight; }
    }
}
