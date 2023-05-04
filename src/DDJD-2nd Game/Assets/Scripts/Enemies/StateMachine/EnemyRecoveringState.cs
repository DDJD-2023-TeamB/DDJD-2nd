using UnityEngine;
using System.Collections.Generic;

public class EnemyRecoveringState : GenericState
{
    protected BasicEnemy _context;
    private string _recoveringAnimationName;
    private List<float> _layerWeights = new List<float>();

    public EnemyRecoveringState(BasicEnemy enemy, string recoveringAnimationName)
        : base(enemy)
    {
        _context = enemy;
        _recoveringAnimationName = recoveringAnimationName;
    }

    public override void Enter()
    {
        // set all layers to 0 expect first
        _context.RagdollController.DeactivateRagdoll();
        _context.Animator.Play(_recoveringAnimationName);
        for (int i = 1; i < _context.Animator.layerCount; i++)
        {
            _layerWeights.Add(_context.Animator.GetLayerWeight(i));
            _context.Animator.SetLayerWeight(i, 0);
        }
    }

    public override void StateUpdate()
    {
        if (!_context.Animator.GetCurrentAnimatorStateInfo(0).IsName(_recoveringAnimationName))
        {
            _context.ChangeState(_context.States.IdleState);
            return;
        }
    }

    public override void Exit()
    {
        base.Exit();
        _context.NavMeshAgent.enabled = false;
    }

    public override bool CanChangeState(GenericState state)
    {
        return true;
    }
}
