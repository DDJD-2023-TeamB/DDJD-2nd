using UnityEngine;
using System.Collections;

public class InteractingState : GenericState
{
    private Player _context;

    public InteractingState(StateContext context, GenericState superState)
        : base(context, superState)
    {
        _context = (Player)context;
    }

    public override void Enter()
    {
        Interactable objt = _context.InteractedObject;
        _context.Animator.SetFloat("ForwardSpeed", 0.0f);
        _context.Animator.SetFloat("RightSpeed", 0.0f);
        objt.Interact();
        HelpManager.Instance.SetHelpText("");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override bool CanChangeState(GenericState state)
    {
        if (!base.CanChangeState(state))
        {
            return false;
        }
        return true;
    }

    public override void StateUpdate()
    {

        Interactable objt = _context.InteractedObject;
        if (_context.Input.IsContinueReading && objt is Npc)
        {
            Npc npc = (Npc)objt;
            npc.ContinueInteraction();
        }

        if (CheckCurrentStates())
        {
            return;
        }
    }

    // Check state and substate, return true if state is changed
    private bool CheckCurrentStates()
    {
        return false;
    }

}
