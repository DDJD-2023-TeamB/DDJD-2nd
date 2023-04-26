using UnityEngine;

public class HoverMovementComponent : AirMovementComponent
{
    private HoverMovementSkill _skill;

    public override void SetSkill(AirMovementSkill skill)
    {
        base.SetSkill(skill);
        _skill = (HoverMovementSkill)skill;
    }

    public override void OnKeyDown() { }

    public override void OnKeyUp() { }

    public override void Update() { }
}
