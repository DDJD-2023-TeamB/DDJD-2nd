using UnityEngine;

public class EnemyDeadState : GenericState
{
    protected BasicEnemy _context;

    public EnemyDeadState(BasicEnemy enemy)
        : base(enemy)
    {
        _context = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void StateUpdate()
    {
        //Doesn't update
    }

    public override void Exit()
    {
        base.Exit();
        //Doesn't exit
    }

    public override bool CanChangeState(GenericState state)
    {
        return false;
    }
}
