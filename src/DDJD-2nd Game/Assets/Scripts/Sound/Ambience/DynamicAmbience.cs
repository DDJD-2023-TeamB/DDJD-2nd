using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundParameter
{
    private FMOD.Studio.PARAMETER_ID _soundParameter;
    private string _eventName;
    private string _parameterName;

    public SoundParameter(
        string parameterName,
        FMOD.Studio.PARAMETER_ID soundParameter,
        string eventName
    )
    {
        _parameterName = parameterName;
        _soundParameter = soundParameter;
        _eventName = eventName;
    }

    public override int GetHashCode()
    {
        return _soundParameter.GetHashCode();
    }

    public string EventName
    {
        get => _eventName;
    }

    public FMOD.Studio.PARAMETER_ID SoundParameterId
    {
        get => _soundParameter;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as SoundParameter);
    }

    public bool Equals(SoundParameter obj)
    {
        return obj != null
            && obj._parameterName == this._parameterName
            && obj._eventName == this._eventName;
    }
}

public class DynamicAmbience : MonoBehaviour
{
    private Dictionary<SoundParameter, List<SoundLocation>> _soundParameters =
        new Dictionary<SoundParameter, List<SoundLocation>>();
    private SoundEmitter _soundEmitter;

    [SerializeField]
    private List<float> _values = new List<float>();

    private void Awake()
    {
        _soundEmitter = GetComponent<SoundEmitter>();
    }

    private void Start()
    {
        //Find children with SouldLocation
        SoundLocation[] soundLocations = GetComponentsInChildren<SoundLocation>();
        foreach (SoundLocation soundLocation in soundLocations)
        {
            SoundParameter soundParameter = new SoundParameter(
                soundLocation.SoundParameter,
                soundLocation.GetParameterId(),
                soundLocation.EventName
            );
            if (!_soundParameters.ContainsKey(soundParameter))
            {
                _soundParameters.Add(soundParameter, new List<SoundLocation>());
            }
            _soundParameters[soundParameter].Add(soundLocation);
        }
        StartCoroutine(UpdateSoundParameters());
    }

    private IEnumerator UpdateSoundParameters()
    {
        while (true)
        {
            _values.Clear();
            yield return new WaitForSeconds(0.25f);
            foreach (
                KeyValuePair<SoundParameter, List<SoundLocation>> soundParameter in _soundParameters
            )
            {
                float value = 0.0f;
                foreach (SoundLocation soundLocation in soundParameter.Value)
                {
                    if (soundLocation.IsPlayerInRange())
                    {
                        value += !soundLocation.IsCloser
                            ? soundLocation.GetDistanceToCenterNormalized()
                            : soundLocation.GetDistanceToCenterNormalizedInverted();
                    }
                }
                value = Mathf.Clamp(value, 0.0f, 1.0f);
                _soundEmitter.SetParameter(
                    soundParameter.Key.EventName,
                    soundParameter.Key.SoundParameterId,
                    value
                );
                _values.Add(value);
            }
            yield return new WaitForSeconds(1.0f);
        }
    }
}
