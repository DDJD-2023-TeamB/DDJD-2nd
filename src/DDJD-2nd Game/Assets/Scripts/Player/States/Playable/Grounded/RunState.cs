using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Unity.Mathematics;

public class RunState : GenericState
{
    private Player _context;
    private float _currentSpeed;

    public RunState(StateContext context, GenericState superState)
        : base(context, superState)
    {
        _context = (Player)context;
    }

    public override void Enter()
    {
        base.Enter();
        _context.CharacterMovement.IsRunning = true;
        _context.Dashable.UpdateMaxSpeed(_context.CharacterMovement.GetCurrentMaxSpeed());
    }

    public override void Exit()
    {
        base.Exit();
        _context.CharacterMovement.IsRunning = false;
        _context.Animator.speed = 1f;
        _context.Dashable.UpdateMaxSpeed(_context.CharacterMovement.GetCurrentMaxSpeed());
        _context.CameraController.ChangeFov(_context.CameraController.WalkFov, 0.2f);
    }

    public override bool CanChangeState(GenericState state)
    {
        if (!base.CanChangeState(state))
        {
            return false;
        }
        return true;
    }

    public override bool CanHaveSuperState(GenericState state)
    {
        if (!base.CanHaveSuperState(state))
        {
            return false;
        }
        if (state is AimingState)
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

        Vector2 moveInput = _context.Input.MoveInput;
        Accelerate(moveInput);
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
        float maxSpeed = _context.CharacterMovement.MaxAnimationSpeed;
        float animationSpeed = math.remap(
            0.0f,
            _context.CharacterMovement.GetCurrentMaxSpeed(),
            1.0f,
            maxSpeed,
            _context.Rigidbody.velocity.magnitude
        );
        animationSpeed = math.clamp(animationSpeed, 1.0f, maxSpeed);
        _context.Animator.speed = animationSpeed;
        ChangeFov();
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

    private void Decelerate() { }

    private void Accelerate(Vector2 moveInput)
    {
        Vector3 velocity = _context.Rigidbody.velocity;
        Vector3 moveDirection =
            _context.transform.forward * moveInput.y + _context.transform.right * moveInput.x;

        if (velocity.magnitude < _context.CharacterMovement.MaxRunSpeed)
        {
            _context.Rigidbody.AddForce(
                moveDirection * _context.CharacterMovement.Acceleration * Time.deltaTime * 1000,
                ForceMode.Acceleration
            );
        }
    }

    private void ChangeFov()
    {
        float fov = math.remap(
            _context.CharacterMovement.MaxSpeed,
            _context.CharacterMovement.MaxRunSpeed,
            _context.CameraController.WalkFov,
            _context.CameraController.RunFov,
            _context.Rigidbody.velocity.magnitude
        );
        fov = math.clamp(fov, _context.CameraController.WalkFov, _context.CameraController.RunFov);
        _context.CameraController.ChangeFov(fov);
    }
}
