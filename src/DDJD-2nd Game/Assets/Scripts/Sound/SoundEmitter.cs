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

    private Dictionary<string, GameObject> _eventPositions = new Dictionary<string, GameObject>();

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
            if (sound.UpdatePosition)
            {
                StartCoroutine(UpdatePosition(pair.Key, eventInstance));
            }
        }
    }

    public void SetEventPositionToFollow(string eventName, GameObject position)
    {
        _eventPositions[eventName] = position;
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

    public void SetParameter(
        string eventName,
        FMOD.Studio.PARAMETER_ID id,
        float value,
        bool start = false
    )
    {
        if (_eventInstances.ContainsKey(eventName))
        {
            _eventInstances[eventName].setParameterByID(id, value);
            if (start)
            {
                _eventInstances[eventName].start();
            }
        }
    }

    public void SetParameter(
        string eventName,
        FMOD.Studio.PARAMETER_ID id,
        int value,
        bool start = false
    )
    {
        if (_eventInstances.ContainsKey(eventName))
        {
            _eventInstances[eventName].setParameterByID(id, value);
            if (start)
            {
                _eventInstances[eventName].start();
            }
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

    public void OnDestroy()
    {
        foreach (KeyValuePair<string, FMOD.Studio.EventInstance> pair in _eventInstances)
        {
            string eventName = pair.Key;
            if (_events[eventName].StopOnDestroy)
            {
                StopAndRelease(eventName);
            }
            else
            {
                _eventInstances[eventName].release();
            }
        }
    }

    public IEnumerator UpdatePosition(string eventName, FMOD.Studio.EventInstance eventInstance)
    {
        while (true)
        {
            GameObject positionObject = _eventPositions.GetValueOrDefault(eventName) ?? gameObject;
            eventInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(positionObject));
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void UpdatePosition(string eventName)
    {
        if (_eventInstances.ContainsKey(eventName))
        {
            GameObject positionObject = _eventPositions.GetValueOrDefault(eventName) ?? gameObject;
            _eventInstances[eventName].set3DAttributes(
                FMODUnity.RuntimeUtils.To3DAttributes(positionObject)
            );
        }
    }

    public static void PlayOneShot(
        FMODUnity.EventReference eventReference,
        string parameterName,
        string parameterValue,
        Vector3 position = new Vector3()
    )
    {
        FMOD.Studio.EventInstance instance = FMODUnity.RuntimeManager.CreateInstance(
            eventReference
        );
        instance.setParameterByNameWithLabel(parameterName, parameterValue);
        instance.start();
        instance.release();
    }
}
