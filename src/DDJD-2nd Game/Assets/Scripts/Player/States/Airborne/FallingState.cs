using UnityEngine;
using System.Collections;

public class FallingState : GenericState
{
    private Player _context;

    public FallingState(StateContext context, GenericState superState) : base(context, superState)
    { 
        _context = (Player)context;
    }

    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        
    }

    public override bool CanChangeState(GenericState state)
    {
        return true;
    }

    public override void StateUpdate()
    {
        if(CheckCurrentStates()){
            return;
        }        

    }

    // Check state and substate, return true if state is changed
    private bool CheckCurrentStates(){
        if(!AimingState.GiveSubState(this, _context)){
            NotAimingState.GiveSubState(this, _context);
        }
        return false;
    }
}