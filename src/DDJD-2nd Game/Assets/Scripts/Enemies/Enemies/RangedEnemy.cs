using UnityEngine;

public class RangedEnemy : BasicEnemy
{
    [Header("Ranged Enemy Settings")]
    [SerializeField]
    private GameObject _leftSpellOrigin;

    [SerializeField]
    private GameObject _rightSpellOrigin;

    private EnemyShooter _shooter;

    private EnemyAimComponent _aimComponent;

    public override void Awake()
    {
        base.Awake();
        _shooter = GetComponent<EnemyShooter>();
        _aimComponent = GetComponent<EnemyAimComponent>();
    }

    public EnemyShooter Shooter
    {
        get { return _shooter; }
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
