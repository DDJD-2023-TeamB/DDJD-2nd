using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthWallComponent : StaticSkillComponent
{
    private RaycastHit? _previousHit = null;
    private Animator _animator;

    private FMOD.Studio.PARAMETER_ID _sfxStateId;

    protected override void Awake()
    {
        base.Awake();
        _collider.enabled = false;
        _animator = GetComponent<Animator>();
    }

    public void Start()
    {
        _sfxStateId = _soundEmitter.GetParameterId("wall", "Earth Wall");
    }

    public override bool CanShoot(Vector3 direction)
    {
        _previousHit = GetTarget(direction);
        return _previousHit != null;
    }

    public override void Shoot(Vector3 direction)
    {
        base.Shoot(direction);

        // Draw a raycast from the player to the mouse position


        _previousHit = _previousHit != null ? _previousHit : GetTarget(direction);
        if (_previousHit != null)
        {
            RaycastHit hit = _previousHit.Value;
            transform.position = hit.point;
            transform.localRotation = Quaternion.LookRotation(
                _caster.transform.forward,
                hit.normal
            );
            _collider.enabled = true;
        }
    }

    private RaycastHit? GetTarget(Vector3 direction)
    {
        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, 100f);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject == gameObject)
            {
                continue;
            }
            return hit;
        }
        return null;
    }

    public override void DestroySpell()
    {
        _soundEmitter.SetParameterWithLabel("wall", _sfxStateId, "Destroy", true);
        _animator.SetTrigger("EndWall");
        Destroy(gameObject, 1.5f);
    }
}
