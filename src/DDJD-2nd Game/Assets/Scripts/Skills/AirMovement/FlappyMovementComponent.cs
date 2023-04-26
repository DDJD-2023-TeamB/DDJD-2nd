using UnityEngine;
using UnityEngine.VFX;

public class FlappyMovementComponent : AirMovementComponent
{
    private FlappyMovementSkill _skill;
    private Transform _footTransform;

    public override void SetSkill(AirMovementSkill skill)
    {
        base.SetSkill(skill);
        _skill = (FlappyMovementSkill)skill;
        _footTransform = _player.Animator.GetBoneTransform(HumanBodyBones.LeftFoot);
    }

    public override void OnKeyDown()
    {
        base.OnKeyDown();
        _rb.velocity = Vector3.zero;
        Vector3 moveDirection = GetMovementDirection();
        _rb.AddForce(Vector3.up * _skill.FlappySkillStats.UpwardForce, ForceMode.Acceleration);
        _rb.AddForce(moveDirection * _skill.FlappySkillStats.ForwardForce, ForceMode.Acceleration);
        GameObject vfx = Instantiate(
            _skill.SpellPrefab,
            _footTransform.position,
            Quaternion.identity
        );
        vfx.GetComponent<VisualEffect>().SetVector3("MoveDirection", moveDirection);
        Destroy(vfx, 2.0f);
    }

    public override void OnKeyUp()
    {
        base.OnKeyUp();
    }

    public override void Update()
    {
        base.Update();
    }
}
