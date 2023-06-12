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
        _context.RagdollController.ActivateRagdoll();
        _context.Animator.enabled = false;
        _context.NavMeshAgent.enabled = false;
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
