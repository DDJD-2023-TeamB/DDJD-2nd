using FMOD;
using System.Diagnostics;
using UnityEngine;

public class ChangeSpellState : MovableState
{
    public ChangeSpellState(StateContext context, GenericState superState)
        : base(context, superState) { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        //AimedSkill rightSkill = _context.UIController.GetRightSkillSelected();
        //AimedSkill leftSkill = _context.UIController.GetLeftSkillSelected();

        //_context.PlayerSkills.LeftSkill = leftSkill;
        //_context.PlayerSkills.RightSkill = rightSkill;

        //_context.UIController.OpenLeftSpell(false);
        //_context.UIController.OpenRightSpell(false);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        if (!_context.Input.IsLeftShooting && !_context.Input.IsRightShooting)
        {
            _context.ChangeState(_context.Factory.Playable());
            return;
        }

        _context.UIController.OpenLeftSpell(_context.Input.IsLeftShooting);
        _context.UIController.OpenRightSpell(_context.Input.IsRightShooting);
    }
}
