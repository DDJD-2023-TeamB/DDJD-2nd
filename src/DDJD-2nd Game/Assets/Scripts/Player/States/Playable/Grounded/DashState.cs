using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : GenericState
{
    private Player _context;
    private DashStats _stats;
    private Dash _skill;
    private Rigidbody _rigidbody;

    public DashState(StateContext context, GenericState superState, DashStats stats, Dash skill)
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
        if (_skill != null)
        {
            dashable.DashWithSkill(_skill);
            // TODO only use dash when state is not active, might not be a need for skill cooldown
            // Dash probably doesn't even need to be a skill?
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
        // TODO no fucking clue
    }

    public override bool CanChangeState(GenericState state)
    {
        if (!base.CanChangeState(state))
        {
            return false;
        }
        // TODO no fucking clue
        return true;
    }

    public override bool CanHaveSuperState(GenericState state)
    {
        if (!base.CanHaveSuperState(state))
        {
            return false;
        }
        // TODO no fucking clue
        return true;
    }

    public override void StateUpdate() {
        // TODO no fucking clue
    }
}
