using UnityEngine;

public abstract class EnemyState : GenericState
{
    protected BasicEnemy _context;

    public EnemyState(BasicEnemy context)
        : base(context)
    {
        _context = context;
    }

    public override void Enter()
    {
        base.Enter();
        _context.EnemyCommunicator.SetMessageAction(typeof(MoveToMessage), MoveTo);
    }

    public override void Exit()
    {
        base.Exit();
        _context.EnemyCommunicator.DeleteAction(typeof(MoveToMessage), MoveTo);
    }

    public override void StateUpdate() { }

    private void MoveTo(EnemyMessage message)
    {
        Debug.Log("Moving to");
        MoveToMessage moveToMessage = (MoveToMessage)message;
        _context.ChangeState(new EnemyMoveToState(_context, moveToMessage.Position));
    }
}
