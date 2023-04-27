using UnityEngine;
using System.Collections;
using UnityEngine.VFX;

public class HoverMovementComponent : AirMovementComponent
{
    private HoverMovementSkill _skill;
    private bool _isHovering = false;
    private float _updateRate;

    private Transform _leftHand;
    private Transform _rightHand;

    private GameObject _leftHandVFX;
    private GameObject _rightHandVFX;

    public override void SetSkill(AirMovementSkill skill)
    {
        base.SetSkill(skill);
        _skill = (HoverMovementSkill)skill;
        _updateRate = _skill.HoverSkillStats.UpdateRate;

        _leftHand = _player.Animator.GetBoneTransform(HumanBodyBones.LeftHand);
        _rightHand = _player.Animator.GetBoneTransform(HumanBodyBones.RightHand);
    }

    public override void OnKeyDown()
    {
        _isHovering = true;
        _leftHandVFX = Instantiate(_skill.SpellPrefab, _leftHand.position, Quaternion.identity);
        _leftHandVFX.transform.parent = _leftHand;
        _rightHandVFX = Instantiate(_skill.SpellPrefab, _rightHand.position, Quaternion.identity);
        _rightHandVFX.transform.parent = _rightHand;

        StartCoroutine(Hover());
    }

    public override void OnKeyUp()
    {
        if (_leftHandVFX != null)
        {
            _leftHandVFX.GetComponent<VisualEffect>().Stop();
            Destroy(_leftHandVFX, 2.0f);
            _leftHandVFX = null;
        }
        if (_rightHandVFX != null)
        {
            _rightHandVFX.GetComponent<VisualEffect>().Stop();
            Destroy(_rightHandVFX, 2.0f);
            _rightHandVFX = null;
        }

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
