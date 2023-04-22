using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : MeleeAttackableState
{
    private Player _context;
    private DashStats _stats;
    private DashSkill _skill;
    private Rigidbody _rigidbody;
    private bool _isDashOver = false;

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
            _context.StartCoroutine(OnDashEnd(_skill.DashStats.Duration));
        }
        else
        {
            dashable.Dash(_stats);
            _context.StartCoroutine(OnDashEnd(_stats.Duration));
        }
    }

    public override bool CanChangeState(GenericState state)
    {
        if (!base.CanChangeState(state))
        {
            return false;
        }
        return _isDashOver;
    }

    private IEnumerator OnDashEnd(float duration)
    {
        yield return new WaitForSeconds(duration);
        _isDashOver = true;
        yield return null;
    }
}
