public class MeleeEnemy : BasicEnemy
{
    public override void Awake()
    {
        base.Awake();
        _states = new EnemyStates(
            chaseState: new EnemySimpleMovementState(this),
            attackState: new MeleeAttackState(this),
            idleState: new EnemyIdleState(this)
        );
    }
}
