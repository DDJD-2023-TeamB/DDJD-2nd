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

    public override void Awake()
    {
        base.Awake();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _states = new EnemyStates(new EnemyChaseState(this), new EnemyAttackState(this));
    }

    public override void Start()
    {
        base.Start();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Debug.Log("Player: " + _player);
        _state = new EnemyIdleState(this);
    }

    public override void Die(int force, Vector3 hitPoint, Vector3 hitDirection)
    {
        base.Die(force, hitPoint, hitDirection);
        _navMeshAgent.enabled = false;
    }

    public override void Update()
    {
        base.Update();
        _state.StateUpdate();
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
}
