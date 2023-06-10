using UnityEngine;
using System.Collections;

public class GroundedState : MeleeAttackableState
{
    private Player _context;

    public GroundedState(StateContext context, GenericState superState)
        : base(context, superState)
    {
        _context = (Player)context;
    }

    public override void Enter()
    {
        base.Enter();
        _context.Animator.SetBool("IsGrounded", true);
        ChangeSubState(_context.Factory.Idle(this));
        _context.StartCoroutine(LandCoroutine());
    }

    private void CheckAbsorb()
    {
        if (_context.Input.IsAbsorbing && _context.ElementController.CanAbsorb())
        {
            _context.ChangeState(_context.Factory.Absorbing(null));
        }
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        CheckAbsorb();

        if (_context.Input.IsJumping)
        {
            _context.Rigidbody.AddForce(
                Vector3.up * _context.JumpForce * Time.deltaTime * 10,
                ForceMode.Acceleration
            );
            _context.Animator.SetTrigger("Jump");
            _context.SoundEmitter.SetParameterWithLabel(
                "jump",
                _context.SfxJumpStateId,
                "Jump",
                true
            );
            return;
        }

        CheckMoving();
    }

    private bool CheckMoving()
    {
        if (_substate is MoveState)
        {
            if (_context.Input.IsRunning)
            {
                bool success = ChangeSubState(_context.Factory.Run(this));
                if (success)
                {
                    return true;
                }
            }
        }
        if (_substate is RunState)
        {
            if (_context.Input.MoveInput == Vector2.zero || !_context.Input.IsRunning)
            {
                ChangeSubState(_context.Factory.Move(this));
                return true;
            }
        }
        if (_substate is MeleeAttackingState)
        {
            if (!_context.Input.IsMeleeAttacking)
            {
                ChangeSubState(_context.Factory.Idle(this));
                return true;
            }
        }
        if (_substate is IdleState)
        {
            if (
                _context.Input.MoveInput != Vector2.zero
                || _context.Rigidbody.velocity.magnitude > 0.1f
            )
            {
                ChangeSubState(_context.Factory.Move(this));
                return true;
            }
        }
        if (!(_substate is IdleState))
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

    private IEnumerator LandCoroutine()
    {
        _context.Collider.material = _context.FrictionlessMaterial;
        while (true)
        {
            yield return new WaitForSeconds(0.6f);
            _context.Collider.material = _context.DefaultMaterial;
        }
    }
}
