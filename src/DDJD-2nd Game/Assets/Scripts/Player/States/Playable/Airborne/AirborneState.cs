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
        if (
            _context.Input.IsJumping
            && (_context.AirMovement != null && _context.AirMovement.IsActive())
        )
        {
            OnJumpKeyDown();
        }
        _context.AirborneComponent.StartAirborne();
    }

    public override void Exit()
    {
        base.Exit();
        OnJumpKeyUp();
        _context.Animator.SetBool("IsGrounded", true);
        _context.Input.OnJumpKeyUp -= OnJumpKeyUp;
        _context.Input.OnJumpKeyDown -= OnJumpKeyDown;

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
            _context.Footsteps.Play();
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

        Vector2 moveInput = _context.Input.MoveInput;
        if (moveInput == Vector2.zero)
        {
            Decelerate();
        }

        if (moveInput != Vector2.zero)
        {
            Accelerate(moveInput);
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

    private void Decelerate(float multiplier = 1.0f)
    {
        Vector3 velocity = _context.Rigidbody.velocity;
        Vector3 horizontalVelocity = new Vector3(velocity.x, 0, velocity.z);
        if (horizontalVelocity.magnitude > 0.1f)
        {
            _context.Rigidbody.AddForce(
                -horizontalVelocity.normalized
                    * _context.AirAcceleration
                    * Time.deltaTime
                    * multiplier,
                ForceMode.Acceleration
            );
        }
        else
        {
            velocity = new Vector3(0, velocity.y, 0);
        }
    }

    private void Accelerate(Vector2 moveInput)
    {
        Vector3 velocity = _context.Rigidbody.velocity;
        Vector3 horizontalVelocity = new Vector3(velocity.x, 0, velocity.z);
        if (horizontalVelocity.magnitude > _context.MaxAirSpeed) // velocity might exceed because of previous state
        {
            Decelerate(_context.AccelerationMultiplier);
        }

        Vector3 moveDirection =
            _context.transform.forward * moveInput.y + _context.transform.right * moveInput.x;

        _context.Rigidbody.AddForce(
            moveDirection
                * _context.AirAcceleration
                * Time.deltaTime
                * _context.AccelerationMultiplier,
            ForceMode.Acceleration
        );
    }
}
