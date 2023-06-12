using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MoveState : GenericState
{
    private Player _context;
    private float _currentSpeed;

    public MoveState(StateContext context, GenericState superState)
        : base(context, superState)
    {
        _context = (Player)context;
    }

    public override void Enter()
    {
        base.Enter();
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
        if (CheckCurrentStates())
        {
            return;
        }

        if (_context.Rigidbody.velocity.magnitude > _context.CharacterMovement.GetCurrentMaxSpeed())
        {
            float difference =
                _context.Rigidbody.velocity.magnitude
                - _context.CharacterMovement.GetCurrentMaxSpeed();
            Decelerate(difference / 10.0f);
        }

        Vector2 moveInput = _context.Input.MoveInput;
        if (moveInput == Vector2.zero)
        {
            Decelerate(1.0f);
        }

        if (moveInput != Vector2.zero)
        {
            Accelerate(moveInput);
        }
        _context.Animator.SetFloat(
            "ForwardSpeed",
            Vector3.Dot(_context.Rigidbody.velocity, _context.transform.forward)
                / _context.CharacterMovement.MaxSpeed
        );
        _context.Animator.SetFloat(
            "RightSpeed",
            Vector3.Dot(_context.Rigidbody.velocity, _context.transform.right)
                / _context.CharacterMovement.MaxSpeed
        );
    }

    // Check state and substate, return true if state is changed
    private bool CheckCurrentStates()
    {
        if (_context.Input.MoveInput == Vector2.zero)
        {
            if (_context.Rigidbody.velocity.magnitude < 0.1f)
            {
                _superstate.ChangeSubState(_context.Factory.Idle(_superstate));
                return true;
            }
        }
        return false;
    }

    private void Decelerate(float multiplier)
    {
        Vector3 velocity = _context.Rigidbody.velocity;
        _context.Rigidbody.AddForce(
            -velocity.normalized
                * multiplier
                * _context.CharacterMovement.Acceleration
                * Time.deltaTime
                * _context.AccelerationMultiplier,
            ForceMode.Acceleration
        );
    }

    private void Accelerate(Vector2 moveInput)
    {
        Vector3 velocity = _context.Rigidbody.velocity;
        Vector3 moveDirection =
            _context.transform.forward * moveInput.y + _context.transform.right * moveInput.x;
        if (velocity.magnitude < _context.CharacterMovement.MaxSpeed)
        {
            _context.Rigidbody.AddForce(
                moveDirection
                    * _context.CharacterMovement.Acceleration
                    * Time.deltaTime
                    * _context.AccelerationMultiplier,
                ForceMode.Acceleration
            );
        }
    }
}
