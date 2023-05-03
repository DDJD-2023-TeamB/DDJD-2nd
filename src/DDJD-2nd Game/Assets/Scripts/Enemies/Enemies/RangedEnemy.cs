using UnityEngine;

public class RangedEnemy : BasicEnemy
{
    [Header("Ranged Enemy Settings")]
    private float _attackSpeed = 1.0f;

    [SerializeField]
    private GameObject _leftSpellOrigin;

    [SerializeField]
    private GameObject _rightSpellOrigin;

    private Shooter _shooter;

    private EnemyAimComponent _aimComponent;

    public override void Awake()
    {
        base.Awake();
        _shooter = GetComponent<Shooter>();
        _aimComponent = GetComponent<EnemyAimComponent>();
        _states = new EnemyStates(new EnemySimpleMovementState(this), new RangedAttackState(this));
    }

    public Shooter Shooter
    {
        get { return _shooter; }
    }

    public float AttackSpeed
    {
        get { return _attackSpeed; }
    }

    public GameObject LeftSpellOrigin
    {
        get { return _leftSpellOrigin; }
    }

    public GameObject RightSpellOrigin
    {
        get { return _rightSpellOrigin; }
    }

    public EnemyAimComponent AimComponent
    {
        get { return _aimComponent; }
    }
}
