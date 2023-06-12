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

    private HoverComponent _leftHandVFX;
    private HoverComponent _rightHandVFX;

    private Coroutine _hoverCoroutine;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void SetSkill(AirMovementSkill skill)
    {
        base.SetSkill(skill);
        _skill = (HoverMovementSkill)skill;
        _updateRate = _skill.HoverSkillStats.UpdateRate;

        _leftHand = _player.Animator.GetBoneTransform(HumanBodyBones.LeftHand);
        _rightHand = _player.Animator.GetBoneTransform(HumanBodyBones.RightHand);

        _leftHandVFX = Instantiate(_skill.SpellPrefab, _leftHand.position, Quaternion.identity)
            .GetComponent<HoverComponent>();
        _leftHandVFX.transform.parent = _leftHand;
        _rightHandVFX = Instantiate(_skill.SpellPrefab, _rightHand.position, Quaternion.identity)
            .GetComponent<HoverComponent>();
        _rightHandVFX.transform.parent = _rightHand;
        _leftHandVFX.Element = _player.PlayerSkills.CurrentElement;
        _rightHandVFX.Element = _player.PlayerSkills.CurrentElement;
    }

    public override void OnKeyDown()
    {
        _isHovering = true;
        _leftHandVFX.Activate();
        _rightHandVFX.Activate();
        _hoverCoroutine = StartCoroutine(Hover());
        Debug.Log("OnKeyDown");
    }

    public override void OnKeyUp()
    {
        Reset();
        if (_hoverCoroutine != null)
        {
            StopCoroutine(_hoverCoroutine);
        }
        _isHovering = false;
    }

    public override void Update()
    {
        if (_isHovering)
        {
            if (_leftHandVFX != null)
            {
                _leftHandVFX.SetVelocity(_player.Rigidbody.velocity);
            }
            if (_rightHandVFX != null)
            {
                _rightHandVFX.SetVelocity(_player.Rigidbody.velocity);
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
            _leftHandVFX.Stop();
        }
        if (_rightHandVFX != null)
        {
            _rightHandVFX.Stop();
        }
    }
}
