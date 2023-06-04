using UnityEngine;

public class AbsorbingState : GenericState
{
    private Player _context;

    public AbsorbingState(StateContext context, GenericState superState)
        : base(context, superState)
    {
        _context = (Player)context;
    }

    public override void Enter()
    {
        base.Enter();
        _context.Animator.SetBool("IsAbsorbing", true);
    }

    public override void Exit()
    {
        base.Exit();
        _context.Animator.SetBool("IsAbsorbing", false);
    }

    public override void StateUpdate() { }
}
