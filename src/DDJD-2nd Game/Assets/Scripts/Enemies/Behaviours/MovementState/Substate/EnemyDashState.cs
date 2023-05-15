using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDashState : GenericState
{
    private BasicEnemy _context;
    private DashStats _stats;
    private DashSkill _skill;
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
        _dashable = _context.EnemyDashable;
        _stats = _context.EnemySkills.DashStats;
        _skill = _context.EnemySkills.CurrentElement.DashSkill;
        _context.NavMeshAgent.enabled = false;
        _context.Rigidbody.isKinematic = false;
        bool canUseSkill =
            _skill != null && (_skill.CanDashInAir || MovementUtils.IsGrounded(_context.Rigidbody));
        if (canUseSkill)
        {
            _dashable.DashWithSkill(_dashDirection, _skill);
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
        _context.ChangeState(_nextState);
    }

    public override void Exit()
    {
        _context.NavMeshAgent.enabled = true;
        _context.Rigidbody.isKinematic = true;
        _dashable.OnDashFinish -= OnDashFinish;
    }
}
