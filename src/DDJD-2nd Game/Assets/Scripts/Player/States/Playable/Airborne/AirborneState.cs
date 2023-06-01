using UnityEngine;
using System.Collections;

public class AirborneState : GenericState
{
    private Player _context;

    public AirborneState(StateContext context, GenericState superState)
        : base(context, superState)
    {
        _context = (Player)context;
    }

    public override void Enter()
    {
        base.Enter();
        _context.Animator.ResetTrigger("Jump");
        _context.Animator.SetBool("IsGrounded", false);
        _context.Input.OnJumpKeyDown += OnJumpKeyDown;
        _context.Input.OnJumpKeyUp += OnJumpKeyUp;
    }

    public override void Exit()
    {
        base.Exit();
        _context.Animator.SetBool("IsGrounded", true);
        _context.Input.OnJumpKeyUp -= OnJumpKeyUp;
        _context.Input.OnJumpKeyDown -= OnJumpKeyDown;
        _context.AirMovement?.Reset();

        bool IsGrounded = MovementUtils.IsGrounded(_context.Rigidbody);
        if (IsGrounded)
        {
            _context.SoundEmitter.SetParameterWithLabel(
                "jump",
                _context.SfxJumpStateId,
                "Hit",
                false
            );
            _context.SoundEmitter.SetParameter(
                "jump",
                _context.SfxJumpIntensityId,
                _context.Rigidbody.velocity.magnitude / 15.0f,
                true
            );
            _context.SoundEmitter.Play("footstep");
        }
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
        if (CheckCurrentStates())
        {
            return;
        }
    }

    private bool CheckCurrentStates()
    {
        if (MovementUtils.IsGrounded(_context.Rigidbody))
        {
            _superstate.ChangeSubState(_context.Factory.Grounded(_superstate));
            return true;
        }

        return false;
    }

    private void OnJumpKeyDown()
    {
        _context.AirMovement?.OnKeyDown();
    }

    private void OnJumpKeyUp()
    {
        _context.AirMovement?.OnKeyUp();
    }
}
