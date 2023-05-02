public class BasicEnemy : HumanoidEnemy
{
    private GenericState _currentState;

    private float _attackRange = 10.0f;
    private float _aggroRange = 40.0f;

    public override void Awake()
    {
        base.Awake();
        _currentState = new EnemyIdleState(this);
    }

    public override void Update()
    {
        base.Update();
        _currentState.StateUpdate();
    }
}
