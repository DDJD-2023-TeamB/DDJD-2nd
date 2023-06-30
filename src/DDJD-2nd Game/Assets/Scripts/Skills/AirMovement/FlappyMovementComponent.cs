using UnityEngine;
using UnityEngine.VFX;

public class FlappyMovementComponent : AirMovementComponent
{
    private FlappyMovementSkill _skill;
    private Transform _footTransform;

    private int _jumpsAvailable = 0;

    public override void SetSkill(AirMovementSkill skill)
    {
        base.SetSkill(skill);
        _skill = (FlappyMovementSkill)skill;
        _footTransform = _player.Animator.GetBoneTransform(HumanBodyBones.LeftFoot);
        _jumpsAvailable = _skill.FlappySkillStats.MaxJumps;
    }

    public override void OnKeyDown()
    {
        base.OnKeyDown();
        if (_jumpsAvailable <= 0)
        {
            return;
        }
        _animator.SetTrigger("JumpAir");
    }

    public void Skywalk()
    {
        Vector3 moveDirection = GetMovementDirection();
        _rb.velocity = Vector3.zero;
        _rb.AddForce(Vector3.up * _skill.FlappySkillStats.UpwardForce, ForceMode.Acceleration);
        _rb.AddForce(moveDirection * _skill.FlappySkillStats.ForwardForce, ForceMode.Acceleration);
        GameObject vfx = Instantiate(
            _skill.SpellPrefab,
            _footTransform.position,
            Quaternion.identity
        );
        VisualEffect visualEffect = vfx.GetComponent<VisualEffect>();
        visualEffect.SetVector3("MoveDirection", moveDirection);
        if (_jumpsAvailable == 1)
        {
            visualEffect.SendEvent("Warning");
        }
        else if (_jumpsAvailable == 0)
        {
            visualEffect.SendEvent("Last");
        }
        else
        {
            visualEffect.SendEvent("Regular");
        }
        Destroy(vfx, 2.0f);
        _jumpsAvailable--;
    }

    public override void Reset()
    {
        base.Reset();
        _jumpsAvailable = _skill.FlappySkillStats.MaxJumps;
    }

    public override void OnKeyUp()
    {
        base.OnKeyUp();
    }

    public override void Update()
    {
        base.Update();
    }

    public override bool IsActive()
    {
        return false;
    }
}
