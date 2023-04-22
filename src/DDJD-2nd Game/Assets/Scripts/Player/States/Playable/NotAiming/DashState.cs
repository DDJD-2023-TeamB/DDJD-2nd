using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : MeleeAttackableState
{
    private Player _context;
    private DashStats _stats;
    private DashSkill _skill;
    private Rigidbody _rigidbody;

    public DashState(
        StateContext context,
        GenericState superState,
        DashStats stats,
        DashSkill skill
    )
        : base(context, superState)
    {
        _context = (Player)context;
        _stats = stats;
        _skill = skill;
        _rigidbody = _context.GetComponent<Rigidbody>();
    }

    public override void Enter()
    {
        Dashable dashable = _context.GetComponent<Dashable>();
        if (_skill != null && !_context.PlayerSkills.IsSkillOnCooldown(_skill))
        {
            dashable.DashWithSkill(_skill);
            _context.PlayerSkills.StartSkillCooldown(_skill);
        }
        else
        {
            dashable.Dash(_stats);
        }
    }

    public override void Exit()
    {
        base.Exit();
        // TODO change superstate's substate to grounded or airb
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
    }
}
