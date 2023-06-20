using UnityEngine;

public class DarkAmbience : Ambience
{
    private FMOD.Studio.PARAMETER_ID _whisperParameterId;
    private FMOD.Studio.PARAMETER_ID _fireDroneId;
    private FMOD.Studio.PARAMETER_ID _crowsParameterId;

    protected override void Start()
    {
        base.Start();
        _whisperParameterId = _soundEmitter.GetParameterId("ambience", "Whisper");
        _fireDroneId = _soundEmitter.GetParameterId("ambience", "Fire Drone");
        _crowsParameterId = _soundEmitter.GetParameterId("ambience", "Crows");
    }

    protected override void Update()
    {
        base.Update();
        if (!_isPlayerInRange)
        {
            return;
        }
        Debug.Log("Ola");
        _soundEmitter.SetParameter(
            "ambience",
            _fireDroneId,
            GetDistanceToCenterNormalizedInverted()
        );
        _soundEmitter.SetParameter(
            "ambience",
            _whisperParameterId,
            GetDistanceToCenterNormalizedInverted()
        );
        _soundEmitter.SetParameter("ambience", _crowsParameterId, GetDistanceToCenterNormalized());
    }
}
