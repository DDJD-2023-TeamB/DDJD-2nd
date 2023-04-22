using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashable : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private PlayerInputReceiver _inputReceiver;

    [SerializeField]
    private Transform _playerCam;

    [SerializeField]
    private bool allowAllDirections = true;

    [SerializeField]
    private bool disableGravity = false;

    [SerializeField]
    private bool resetVelocity = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _inputReceiver = GetComponent<PlayerInputReceiver>();
    }

    public void DashWithSkill(DashSkill dashSkill)
    {
        Quaternion rotation = Quaternion.LookRotation(transform.forward);
        GameObject spell = Instantiate(dashSkill.SpellPrefab, transform.position, rotation);
        DashComponent dashComponent = spell.GetComponent<DashComponent>();
        dashComponent.SetCaster(gameObject);
        dashComponent.SetSkill(dashSkill);

        Dash(dashSkill.DashStats);
    }

    public void Dash(DashStats stats)
    {
        Transform forwardTransform;
        if (allowAllDirections)
            forwardTransform = _playerCam;
        else
            forwardTransform = transform;

        Vector3 direction = GetDirection(forwardTransform);
        Vector3 force = direction * stats.Force;

        if (disableGravity)
            _rigidbody.useGravity = false;
        if (resetVelocity)
            _rigidbody.velocity = Vector3.zero;

        _rigidbody.AddForce(force, ForceMode.Impulse);

        Invoke(nameof(ResetDash), stats.Duration);
    }

    private void ResetDash()
    {
        if (disableGravity)
            _rigidbody.useGravity = true;
    }

    private Vector3 GetDirection(Transform forwardTransform)
    {
        Vector2 moveInput = _inputReceiver.MoveInput;
        Vector3 direction = Vector3.zero;
        if (allowAllDirections && moveInput != Vector2.zero)
        {
            direction =
                forwardTransform.forward * moveInput.y + forwardTransform.right * moveInput.x;
        }
        else
        {
            direction = forwardTransform.forward;
        }

        return direction.normalized;
    }
}
