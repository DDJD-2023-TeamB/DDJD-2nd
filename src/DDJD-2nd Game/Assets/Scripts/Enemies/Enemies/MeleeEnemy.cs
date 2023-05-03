public class MeleeEnemy : BasicEnemy
{
    public override void Awake()
    {
        base.Awake();
        _states = new EnemyStates(new EnemySimpleMovementState(this), new MeleeAttackState(this));
    }
}
