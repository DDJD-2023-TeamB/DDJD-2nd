using UnityEngine;
using System.Collections;

public class DeadState : GenericState
{
    private Player _context;

    public DeadState(StateContext context, GenericState superState)
        : base(context, superState)
    {
        _context = (Player)context;
    }

    public void ShowMessage()
    {
        _context.UIController.ShowKeyMessage();
    }

    public override void Enter()
    {
        base.Enter();
        _context.PlayerDeath.Die();
        _context.UIController.ShowDieMessage();
        _context.PlayerDeath.OnRespawnAvailable += ShowMessage;
    }

    public override void Exit()
    {
        base.Exit();
        _context.PlayerDeath.OnRespawnAvailable -= ShowMessage;
        _context.UIController.CloseDieInfo();
        GameManager.Instance.CombatEnd();
    }

    public override void StateUpdate()
    {
        _context.RagdollController.SetPositionToRagdoll();
        CameraMovement();
        if (_context.Input.IsContinueReading && _context.PlayerDeath.CanRespawn)
        {
            _context.PlayerDeath.Respawn();
        }
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
