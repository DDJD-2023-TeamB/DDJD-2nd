using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundEmitter : MonoBehaviour
{
    [SerializeField]
    private SerializedDictionary<string, GameSound> _events =
        new SerializedDictionary<string, GameSound>();

    private Dictionary<string, FMOD.Studio.EventInstance> _eventInstances =
        new Dictionary<string, FMOD.Studio.EventInstance>();

    private void Awake()
    {
        foreach (KeyValuePair<string, GameSound> pair in _events)
        {
            GameSound sound = pair.Value;
            FMOD.Studio.EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance(
                sound.EventReference
            );
            eventInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            if (sound.PlayOnStart)
            {
                eventInstance.start();
            }
            _eventInstances.Add(pair.Key, eventInstance);
        }
    }

    public FMOD.Studio.PARAMETER_ID GetParameterId(string eventName, string parameterName)
    {
        FMOD.Studio.EventDescription eventDescription;
        _eventInstances[eventName].getDescription(out eventDescription);
        FMOD.Studio.PARAMETER_DESCRIPTION parameterDescription;
        eventDescription.getParameterDescriptionByName(parameterName, out parameterDescription);
        return parameterDescription.id;
    }

    public void Play(string eventName)
    {
        if (_eventInstances.ContainsKey(eventName))
        {
            _eventInstances[eventName].start();
        }
    }

    public void PlayAndRelease(string eventName)
    {
        if (_eventInstances.ContainsKey(eventName))
        {
            _eventInstances[eventName].start();
            _eventInstances[eventName].release();
        }
    }

    public void Stop(string eventName)
    {
        if (_eventInstances.ContainsKey(eventName))
        {
            _eventInstances[eventName].stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }

    public void StopAndRelease(string eventName)
    {
        if (_eventInstances.ContainsKey(eventName))
        {
            _eventInstances[eventName].stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            _eventInstances[eventName].release();
        }
    }

    public void StopAndReleaseAll()
    {
        foreach (KeyValuePair<string, FMOD.Studio.EventInstance> pair in _eventInstances)
        {
            pair.Value.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            pair.Value.release();
        }
    }

    public void SetParameter(string eventName, FMOD.Studio.PARAMETER_ID id, float value)
    {
        if (_eventInstances.ContainsKey(eventName))
        {
            _eventInstances[eventName].setParameterByID(id, value);
        }
    }

    public void SetParameterWithLabel(
        string eventName,
        FMOD.Studio.PARAMETER_ID id,
        string value,
        bool start
    )
    {
        if (_eventInstances.ContainsKey(eventName))
        {
            _eventInstances[eventName].setParameterByIDWithLabel(id, value);
            if (start)
            {
                _eventInstances[eventName].start();
            }
        }
    }

    public Coroutine CallWithDelay(Action action, float delay)
    {
        return StartCoroutine(DelayCoroutine(action, delay));
    }

    private IEnumerator DelayCoroutine(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action();
    }
}
