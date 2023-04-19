using UnityEngine;
using System.Collections;

public class MeleeAttackingState : GenericState
{
    private Player _context;

    private int _lastAttackClipHash;

    private int _attackCounter = 0; // 0 -> No attack is playing, Any other number, attack is playing

    public MeleeAttackingState(StateContext context, GenericState superState)
        : base(context, superState)
    {
        _context = (Player)context;
    }

    public override void Enter()
    {
        _context.MeleeCombat.OnAttackStart += OnAttackStart;
        _context.MeleeCombat.OnAttackEnd += OnAttackEnd;

        _context.Rigidbody.velocity = Vector3.ClampMagnitude(_context.Rigidbody.velocity, 5);
    }

    public override void Exit()
    {
        _context.MeleeCombat.OnAttackStart -= OnAttackStart;
        _context.MeleeCombat.OnAttackEnd -= OnAttackEnd;
    }

    public override bool CanChangeState(GenericState state)
    {
        if (_context.Input.IsMeleeAttacking)
        {
            return false;
        }
        if (_attackCounter != 0)
        {
            // If last attack animations hasn't ended, can't change state
            return false;
        }
        return true;
    }

    public override void StateUpdate() { }

    private void OnAttackStart()
    {
        //Push the player in the direction of the Movement
        Vector2 moveInput = _context.Input.MoveInput;
        Vector3 moveDirection =
            _context.transform.forward * moveInput.y + _context.transform.right * moveInput.x;
        if (moveInput != Vector2.zero)
        {
            _context.Rigidbody.AddForce(
                moveDirection * _context.MeleeCombat.AttackPushForce,
                ForceMode.Acceleration
            );
        }
        _attackCounter++;
    }

    private void OnAttackEnd()
    {
        _attackCounter--;
    }
}
