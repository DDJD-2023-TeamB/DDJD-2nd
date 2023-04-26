using UnityEngine;
using System.Collections;

public class HoverMovementComponent : AirMovementComponent
{
    private HoverMovementSkill _skill;
    private bool _isHovering = false;
    private float _updateRate;

    public override void SetSkill(AirMovementSkill skill)
    {
        base.SetSkill(skill);
        _skill = (HoverMovementSkill)skill;
        _updateRate = _skill.HoverSkillStats.UpdateRate;
    }

    public override void OnKeyDown()
    {
        _isHovering = true;
        StartCoroutine(Hover());
    }

    public override void OnKeyUp()
    {
        _isHovering = false;
    }

    public override void Update() { }

    private IEnumerator Hover()
    {
        while (_isHovering)
        {
            Vector3 movementDirection = GetMovementDirection();
            _rb.AddForce(Vector3.up * _skill.HoverSkillStats.UpwardForce, ForceMode.Acceleration);
            _rb.AddForce(
                movementDirection * _skill.HoverSkillStats.ForwardForce,
                ForceMode.Acceleration
            );
            yield return new WaitForSeconds(_updateRate);
        }
    }
}
