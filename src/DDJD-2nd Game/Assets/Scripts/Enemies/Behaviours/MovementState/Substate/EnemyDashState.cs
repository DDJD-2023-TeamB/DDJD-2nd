using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDashState : GenericState
{
    private BasicEnemy _context;
    private DashStats _stats;
    private DashSkill _dashSkill;
    private EnemyDashable _dashable;
    private Vector3 _dashDirection;

    private GenericState _nextState;

    public EnemyDashState(StateContext context, Vector3 dashDirection, GenericState state)
        : base(context)
    {
        _context = (BasicEnemy)context;
        _dashDirection = dashDirection;
        _nextState = state;
    }

    public override void Enter()
    {
        base.Enter();
        _dashable = _context.EnemyDashable;
        _stats = _context.EnemySkills.DashStats;
        _dashSkill = _context.EnemySkills.CurrentElement.DashSkill;
        _context.NavMeshAgent.enabled = false;
        _context.Rigidbody.isKinematic = false;
        bool canUseSkill =
            _dashSkill != null
            && (_dashSkill.CanDashInAir || MovementUtils.IsGrounded(_context.Rigidbody));
        if (canUseSkill)
        {
            _dashable.DashWithSkill(_dashDirection, _dashSkill);
        }
        else
        {
            _dashable.Dash(_dashDirection, _stats);
        }
        _dashable.OnDashFinish += OnDashFinish;
    }

    public override void StateUpdate() { }

    private void OnDashFinish()
    {
        _context.ChangeState(_context.States.AttackState);
    }

    public override void Exit()
    {
        _dashable.OnDashFinish -= OnDashFinish;
    }
}
