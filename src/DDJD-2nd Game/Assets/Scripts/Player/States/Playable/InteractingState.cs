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
        Interactable objt = _context._interactedObject.GetComponent<Interactable>();
        Debug.Log("Interacting", objt);
        objt.Interact();
        Debug.Log("ON ENTER");
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
        Debug.Log("Interactinggggggggggggggggggggg");
        Debug.Log(_context.Input.IsContinueReading);
        Interactable objt = _context._interactedObject.GetComponent<Interactable>();
        if (_context.Input.IsContinueReading && objt is NPCManager) {
            Debug.Log("NEEEEEEEEEEXT");
            NPCManager npc = (NPCManager)objt;
            npc.ContinueInteraction();
        }
    }
}
