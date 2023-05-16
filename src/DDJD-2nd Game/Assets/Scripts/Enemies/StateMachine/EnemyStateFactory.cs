public class EnemyStateFactory
{
    public static EnemyAttackState CreateAttackState(BasicEnemy enemy)
    {
        return new EnemyAttackState(enemy);
    }

    public static EnemyChaseState CreateChaseState(BasicEnemy enemy)
    {
        return new EnemyChaseState(enemy);
    }

    public static EnemyIdleState CreateIdleState(BasicEnemy enemy)
    {
        return new EnemyIdleState(enemy);
    }
}
