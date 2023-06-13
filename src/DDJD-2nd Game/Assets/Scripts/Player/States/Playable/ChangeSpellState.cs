using UnityEngine;

public class ChangeSpellState : MovableState
{
    public ChangeSpellState(StateContext context, GenericState superState)
        : base(context, superState) { }

    public override void Enter()
    {
        base.Enter();
        _context.TimeController.Slowdown();
    }

    public override void Exit()
    {
        base.Exit();
        _context.TimeController.Reset();
        AimedSkill rightSkill = _context.UIController.GetRightSkillSelected();
        AimedSkill leftSkill = _context.UIController.GetLeftSkillSelected();

        if (rightSkill != null)
        {
            _context.PlayerSkills.RightSkill = rightSkill;
        }
        if (leftSkill != null)
        {
            _context.PlayerSkills.LeftSkill = leftSkill;
        }

        _context.UIController.OpenLeftSpell(false);
        _context.UIController.OpenRightSpell(false);
        _context.UIController.UpdateElements(
            leftSkill,
            rightSkill,
            _context.PlayerSkills.CurrentElement
        );
        _context.Status.UpdateMana(leftSkill.Element);
        _context.Status.UpdateMana(rightSkill.Element);
        Time.timeScale = 1.0f;
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
