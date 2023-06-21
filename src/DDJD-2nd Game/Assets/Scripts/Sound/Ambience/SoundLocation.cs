using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class SoundLocation : MonoBehaviour
{
    [SerializeField]
    private string _soundParameter;

    [SerializeField]
    private string _eventName = "ambience";

    [SerializeField]
    [Tooltip("The radius of the circle around the center where the sound is maximum")]
    private float _radiusMax = 30.0f;

    [SerializeField]
    [Tooltip("The radius of the circle around the center where the is starting to change")]
    private float _radiusMin = 75.0f;

    protected bool _isPlayerInRange = false;

    protected Player _player;

    [SerializeField]
    private SoundEmitter _soundEmitter;

    private FMOD.Studio.PARAMETER_ID _soundParameterId;

    [SerializeField]
    [Tooltip("If true, the parameter will be higher when the player is closer to the center")]
    private bool _isCloser = true;

    protected void Awake() { }

    protected virtual void Start()
    {
        //FInd by tag
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //StartCoroutine(CheckRange());
    }

    public FMOD.Studio.PARAMETER_ID GetParameterId()
    {
        _soundParameterId = _soundEmitter.GetParameterId(_eventName, _soundParameter);
        return _soundParameterId;
    }

    protected virtual void Update() { }

    private IEnumerator CheckRange()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            bool inRange = IsPlayerInRange();
            if (inRange)
            {
                _isPlayerInRange = true;
                float value = _isCloser
                    ? GetDistanceToCenterNormalized()
                    : GetDistanceToCenterNormalizedInverted();
                _soundEmitter.SetParameter(_eventName, _soundParameterId, value);
            }
            else if (!inRange && _isPlayerInRange)
            {
                float value = _isCloser ? 0 : 1;
                _soundEmitter.SetParameter(_eventName, _soundParameterId, value);
                _isPlayerInRange = false;
            }
        }
    }

    public bool IsPlayerInRange()
    {
        return GetDistanceToCenter() < _radiusMin;
    }

    public float GetDistanceToCenter()
    {
        return Vector3.Distance(_player.transform.position, transform.position);
    }

    public float GetDistanceToCenterNormalized()
    {
        float normalized = math.remap(_radiusMax, _radiusMin, 0, 1.0f, GetDistanceToCenter());
        return Mathf.Clamp(normalized, 0.0f, 1.0f);
    }

    public float GetDistanceToCenterNormalizedInverted()
    {
        float normalized = math.remap(_radiusMax, _radiusMin, 1.0f, 0, GetDistanceToCenter());

        return Mathf.Clamp(normalized, 0.0f, 1.0f);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radiusMax);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _radiusMin);
    }

    public string SoundParameter
    {
        get => _soundParameter;
    }

    public string EventName
    {
        get => _eventName;
    }

    public FMOD.Studio.PARAMETER_ID SoundParameterId
    {
        get => _soundParameterId;
    }

    public bool IsCloser
    {
        get => _isCloser;
    }
}
