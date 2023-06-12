using UnityEngine;
using System.Collections;

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
        _context.ElementController.StopAbsorb();
        _context.CameraController.ResetCameraRotation();
    }

    public override void StateUpdate()
    {
        if (!_context.Input.IsAbsorbing)
        {
            _context.ChangeState(_context.Factory.Playable());
        }
        CameraMovement();
    }

    private void CameraMovement()
    {
        Vector2 lookInput = _context.Input.LookInput;
        if (lookInput.magnitude > 1000f)
        {
            return;
        }
        _context.CameraController.RotateCamera(lookInput, false);
    }
}
