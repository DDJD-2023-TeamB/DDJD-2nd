using UnityEngine;
using System.Collections;

public class MeleeAttackingState : GenericState
{

    private Player _context;

    public MeleeAttackingState(StateContext context, GenericState superState)
        : base(context, superState)
    {
        _context = (Player)context;
    }

    public override void Enter()
    {
        _context.MeleeCombat.OnAttackStart += OnAttackStart;
    }

    public override void Exit() {
        _context.MeleeCombat.OnAttackStart -= OnAttackStart;
     }

    public override bool CanChangeState(GenericState state)
    {
        
        return true;
    }

    public override void StateUpdate()
    {

    }

    private void OnAttackStart()
    {
        Debug.Log("OnAttackStart");
        //Push the player in the direction of the Movement
        Vector2 moveInput = _context.Input.MoveInput;
        Vector3 moveDirection =
            _context.transform.forward * moveInput.y + _context.transform.right * moveInput.x;
        if (moveInput != Vector2.zero)
        {
            _context.Rigidbody.AddForce(moveDirection * _context.MeleeCombat.AttackPushForce, ForceMode.Acceleration);
        }
    }
}
