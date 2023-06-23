using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthWallComponent : RayCastSkillComponent
{
    private RaycastHit? _previousHit = null;
    private Animator _animator;

    private FMOD.Studio.PARAMETER_ID _sfxStateId;

    private bool _isRising;

    protected override void Awake()
    {
        base.Awake();
        _collider.enabled = false;
        _animator = GetComponent<Animator>();
        _isRising = true;
    }

    public void Start() { }

    public override bool CanShoot(Vector3 direction)
    {
        _previousHit = GetRaycastHit();
        return _previousHit != null;
    }

    public override void Shoot(Vector3 direction)
    {
        base.Shoot(direction);

        // Draw a raycast from the player to the mouse position


        _previousHit = _previousHit != null ? _previousHit : GetRaycastHit();
        if (_previousHit != null)
        {
            RaycastHit hit = _previousHit.Value;
            transform.position = hit.point;
            transform.localRotation = Quaternion.LookRotation(
                _caster.transform.forward,
                Vector3.up
            );

            //Raycast to find ground
            RaycastHit groundHit;
            if (
                Physics.Raycast(
                    transform.position,
                    Vector3.down,
                    out groundHit,
                    10f,
                    LayerMask.GetMask("Default", "Ground", "Wall", "Environment")
                )
            )
            {
                transform.position = groundHit.point;
            }
            _collider.enabled = true;
        }
        _soundEmitter.UpdatePosition("wall");
        _sfxStateId = _soundEmitter.GetParameterId("wall", "Earth Wall");
    }

    protected override void OnImpact(Collider other, float multiplier = 1)
    {
        base.OnImpact(other, multiplier);
        if (!_isRising)
        {
            return;
        }
        Vector3 direction = (other.transform.position - transform.position).normalized;
        direction.y = 0.5f;
        Damage(
            other.gameObject,
            (int)_stats.Damage,
            (int)_stats.ForceWithDamage(),
            transform.position,
            direction
        );
    }

    public override void DestroySpell()
    {
        _soundEmitter.SetParameterWithLabel("wall", _sfxStateId, "Destroy", true);
        _animator.SetTrigger("EndWall");
        Destroy(gameObject, 1.5f);
    }

    public void StopRising()
    {
        _isRising = false;
    }
}
