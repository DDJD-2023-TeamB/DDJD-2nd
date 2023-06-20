using UnityEngine;

public class CalmAmbience : Ambience
{
    private FMOD.Studio.PARAMETER_ID _villageProximityParameterId;

    protected override void Start()
    {
        base.Start();
        _villageProximityParameterId = _soundEmitter.GetParameterId(
            "ambience",
            "Village Proximity"
        );
    }

    protected override void Update()
    {
        base.Update();
        if (!_isPlayerInRange)
        {
            return;
        }
        _soundEmitter.SetParameter(
            "ambience",
            _villageProximityParameterId,
            GetDistanceToCenterNormalized() * 100.0f
        );
    }
}
