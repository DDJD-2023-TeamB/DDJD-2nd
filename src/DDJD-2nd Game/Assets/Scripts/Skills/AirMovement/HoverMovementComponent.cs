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
    private VisualEffect _leftHandVFXEffect;
    private GameObject _rightHandVFX;
    private VisualEffect _rightHandVFXEffect;

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
        _leftHandVFXEffect = _leftHandVFX.GetComponent<VisualEffect>();
        _rightHandVFX = Instantiate(_skill.SpellPrefab, _rightHand.position, Quaternion.identity);
        _rightHandVFX.transform.parent = _rightHand;
        _rightHandVFXEffect = _rightHandVFX.GetComponent<VisualEffect>();

        StartCoroutine(Hover());
    }

    public override void OnKeyUp()
    {
        Reset();

        _isHovering = false;
    }

    public override void Update()
    {
        if (_isHovering)
        {
            if (_leftHandVFXEffect != null)
            {
                _leftHandVFXEffect.SetVector3("Velocity", _player.Rigidbody.velocity);
            }
            if (_rightHandVFXEffect != null)
            {
                _rightHandVFXEffect.SetVector3("Velocity", _player.Rigidbody.velocity);
            }
        }
    }

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

    void OnDestroy()
    {
        Reset();
    }

    public override void Reset()
    {
        if (_leftHandVFX != null)
        {
            _leftHandVFXEffect.Stop();
            Destroy(_leftHandVFX, 2.0f);
        }
        if (_rightHandVFX != null)
        {
            _rightHandVFXEffect.Stop();
            Destroy(_rightHandVFX, 2.0f);
        }
    }
}
