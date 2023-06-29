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
        _context.Input.OnExitKeydown += ExitInteraction;
        _context.Animator.SetFloat("ForwardSpeed", 0.0f);
        _context.Animator.SetFloat("RightSpeed", 0.0f);
        objt.Interact();
        Cursor.visible = false;
    }

    public override void Exit()
    {
        base.Exit();
        _context.Input.OnExitKeydown -= ExitInteraction;
        Cursor.visible = true;
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
            _context.Input.IsContinueReading = false;
            npc.ContinueInteraction();
        }

        if (objt == null)
        {
            _context.ChangeState(_context.Factory.Playable());
        }
    }

    private void ExitInteraction()
    {
        if (_context.InteractedObject is Npc)
        {
            Npc npc = (Npc)_context.InteractedObject;
            npc.ExitInteraction();
        }
        if (_context.InteractedObject == null)
        {
            _context.ChangeState(_context.Factory.Playable());
        }
    }

    public bool IsInteracting()
    {
        return _context.InteractedObject != null;
    }
}
