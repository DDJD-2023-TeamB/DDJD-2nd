using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : CharacterStatus
{
    private Player _player;

    [SerializeField]
    private float _cameraShakeIntensity = 5.0f;

    protected override void Awake()
    {
        base.Awake();
        _player = GetComponent<Player>();
    }

    protected void Start() { }

    public override void TakeDamage(
        GameObject damager,
        int damage,
        Vector3 hitPoint = default(Vector3),
        Vector3 hitDirection = default(Vector3)
    )
    {
        base.TakeDamage(damager, damage, hitPoint, hitDirection);
        float shakeIntensity =
            Mathf.Clamp01((float)damage / (float)_maxHealth) * _cameraShakeIntensity;
        _player.CameraController.ShakeCamera(shakeIntensity, 0.2f);
    }
}
