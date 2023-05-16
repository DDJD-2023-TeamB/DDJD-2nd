using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : MeleeAttackableState
{
    private Player _context;
    private DashStats _stats;
    private DashSkill _skill;
    private Rigidbody _rigidbody;
    private Dashable _dashable;

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
        _dashable = _context.GetComponent<Dashable>();
        bool canUseSkill =
            _skill != null
            && !_context.PlayerSkills.IsSkillOnCooldown(_skill)
            && (_skill.CanDashInAir || MovementUtils.IsGrounded(_context.Rigidbody));
        if (canUseSkill)
        {
            _dashable.DashWithSkill(_skill);
            _context.PlayerSkills.StartSkillCooldown(_skill);
        }
        else
        {
            _dashable.Dash(_stats);
        }
    }

    public override bool CanChangeState(GenericState state)
    {
        if (!base.CanChangeState(state))
        {
            return false;
        }
        return !_dashable.IsDashing;
    }
}
