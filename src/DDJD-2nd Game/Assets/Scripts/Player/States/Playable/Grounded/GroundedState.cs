using UnityEngine;
using System.Collections;

public class GroundedState : GenericState
{
    private Player _context;

    public GroundedState(StateContext context, GenericState superState)
        : base(context, superState)
    {
        _context = (Player)context;
    }

    public override void Enter()
    {
        _context.Animator.SetBool("IsGrounded", true);
        ChangeSubState(_context.Factory.Idle(this));
        CheckMoving();
    }

    public override void Exit() { }

    public override bool CanChangeState(GenericState state)
    {
        return true;
    }

    public override void StateUpdate()
    {
        if (!_substate.CanChangeState(null))
        {
            return;
        }
        if (_context.Input.IsMeleeAttacking && !(_substate is MeleeAttackingState))
        {
            ChangeSubState(_context.Factory.GetMeleeAttackingState(this));
            return;
        }

        if (_context.Input.IsJumping)
        {
            _context.Rigidbody.AddForce(
                Vector3.up * _context.JumpForce * Time.deltaTime * 10,
                ForceMode.Impulse
            );
            _context.Animator.SetTrigger("Jump");
            return;
        }

        CheckMoving();
    }

    private bool CheckMoving()
    {
        if (!(_substate is MoveState))
        {
            if (_context.Input.MoveInput != Vector2.zero)
            {
                ChangeSubState(_context.Factory.Move(this));
                return true;
            }
        }
        else if (!(_substate is IdleState))
        {
            bool isMoving = _context.Rigidbody.velocity.magnitude > 0.1f;
            if (_context.Input.MoveInput == Vector2.zero && !isMoving)
            {
                ChangeSubState(_context.Factory.Idle(this));
                return true;
            }
        }
        return false;
    }
}
