using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DashStats : SkillStats
{
    [SerializeField]
    private float _duration = 0.3f;
    public float Duration
    {
        get => _duration;
        set => _duration = value;
    }

    [SerializeField]
    private float _maxSpeed = 20f;
    public float MaxSpeed
    {
        get => _maxSpeed;
        set => _maxSpeed = value;
    }

    [SerializeField]
    private float _cameraFov = 55f;
    public float CameraFov
    {
        get => _cameraFov;
        set => _cameraFov = value;
    }

    [SerializeField]
    private float _enterFovTime = 0.25f;
    public float EnterFovTime
    {
        get => _enterFovTime;
        set => _enterFovTime = value;
    }

    [SerializeField]
    private float _exitFovTime = 0.25f;
    public float ExitFovTime
    {
        get => _exitFovTime;
        set => _exitFovTime = value;
    }

    public DashStats(
        float damage,
        float cooldown,
        float duration,
        float maxSpeed,
        float cameraFov,
        float enterFovTime,
        float exitFovTime
    )
        : base(damage, cooldown)
    {
        _duration = duration;
        _maxSpeed = maxSpeed;
        _cameraFov = cameraFov;
        _enterFovTime = enterFovTime;
        _exitFovTime = exitFovTime;
    }
}
