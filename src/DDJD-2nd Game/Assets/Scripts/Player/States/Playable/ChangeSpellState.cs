using UnityEngine;

public class ChangeSpellState : MovableState
{
    public ChangeSpellState(StateContext context, GenericState superState)
        : base(context, superState) { }

    public override void Enter()
    {
        base.Enter();
        _context.TimeController.Slowdown();

        if (_context.Input.IsLeftShooting)
        {
            OnLeftKeydown();
        }

        if (_context.Input.IsRightShooting)
        {
            OnRightKeydown();
        }

        _context.Input.OnLeftShootKeydown += OnLeftKeydown;
        _context.Input.OnRightShootKeydown += OnRightKeydown;
        _context.Input.OnLeftShootKeyup += OnLeftKeyup;
        _context.Input.OnRightShootKeyup += OnRightKeyup;
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
    }

    private void OnLeftKeydown()
    {
        _context.UIController.OpenLeftSpell(true);
    }

    private void OnRightKeydown()
    {
        _context.UIController.OpenRightSpell(true);
    }

    private void OnLeftKeyup()
    {
        _context.UIController.OpenLeftSpell(false);
    }

    private void OnRightKeyup()
    {
        _context.UIController.OpenRightSpell(false);
    }
}
