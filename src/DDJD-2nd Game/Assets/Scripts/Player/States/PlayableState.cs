using UnityEngine;
using System.Collections;
using System;
using Unity.Mathematics;

public class PlayableState : GenericState
{
    private Player _context;

    public PlayableState(StateContext context)
        : base(context, null)
    {
        _context = (Player)context;
    }

    public override void Enter()
    {
        base.Enter();
        CheckAiming();
        _context.Input.OnInventoryKeydown += OnInventoryKeydown;
        _context.Input.OnMissionKeydown += OnMissionKeydown;
        _context.Input.OnMenuKeydown += OnMenuKeydown;
    }

    public override void Exit()
    {
        base.Exit();
        _context.Input.OnInventoryKeydown -= OnInventoryKeydown;
        _context.Input.OnMissionKeydown -= OnMissionKeydown;
        _context.Input.OnMenuKeydown -= OnMenuKeydown;
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
        CheckAiming();
        LookRotation();
        CheckRange();
    }

    private void CheckRange()
    {   
        if (
            _context.Input.IsInteracting
            && !(_substate is InteractingState)
            && _context.InteractedObject != null
        )
        {
            ChangeSubState(_context.Factory.Interacting(this));
        }
    }

    private void CheckAiming()
    {
        if (_context.Input.IsAiming && !(_substate is AimingState))
        {
            ChangeSubState(_context.Factory.Aiming(this));
        }
        else if (
            !_context.Input.IsAiming
            && !(_substate is NotAimingState)
            && _context.InteractedObject == null
        )
        {
            ChangeSubState(_context.Factory.NotAiming(this));
        }
    }

    private void LookRotation()
    {
        Vector2 lookInput = _context.Input.LookInput;
        if (lookInput.magnitude > 1000f)
        {
            return;
        }

        _context.CameraController.RotateCamera(lookInput, true);
    }

    private void OnInventoryKeydown()
    {
        _context.ChangeState(_context.Factory.Inventory());
    }

    private void OnMissionKeydown()
    {
        _context.ChangeState(_context.Factory.MissionMenu());
    }

    private void OnMenuKeydown()
    {
        _context.ChangeState(_context.Factory.Menu());
    }
}
