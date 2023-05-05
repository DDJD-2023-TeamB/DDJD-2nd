using UnityEngine;

public class MeleeAttackState : EnemyAttackState
{
    public MeleeAttackState(BasicEnemy enemy)
        : base(enemy) { }

    protected override void Attack() { }

    protected override void Move() { }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Enter()
    {
        base.Enter();
    }
}
