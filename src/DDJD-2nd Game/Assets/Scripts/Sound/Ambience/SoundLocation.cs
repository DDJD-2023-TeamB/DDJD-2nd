using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class SoundLocation : MonoBehaviour
{
    [SerializeField]
    private string _soundParameter;

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
        _soundParameterId = _soundEmitter.GetParameterId(_soundParameter, _soundParameter);
        StartCoroutine(CheckRange());
    }

    protected virtual void Update() { }

    private IEnumerator CheckRange()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            bool inRange = IsPlayerInRange();
            if (inRange && !_isPlayerInRange)
            {
                _isPlayerInRange = true;
                float value = _isCloser
                    ? GetDistanceToCenterNormalized()
                    : GetDistanceToCenterNormalizedInverted();
                _soundEmitter.SetParameter("ambience", _soundParameterId, value);
            }
            else if (!inRange && _isPlayerInRange)
            {
                float value = _isCloser ? 0 : 1;
                _soundEmitter.SetParameter("ambience", _soundParameterId, value);
                _isPlayerInRange = false;
            }
        }
    }

    protected bool IsPlayerInRange()
    {
        return GetDistanceToCenter() < _radiusMin;
    }

    protected float GetDistanceToCenter()
    {
        return Vector3.Distance(_player.transform.position, transform.position);
    }

    protected float GetDistanceToCenterNormalized()
    {
        float normalized = math.remap(_radiusMax, _radiusMin, 0, 1.0f, GetDistanceToCenter());

        return Mathf.Clamp(normalized, 0.0f, 1.0f);
    }

    protected float GetDistanceToCenterNormalizedInverted()
    {
        float normalized = math.remap(_radiusMax, _radiusMin, 1.0f, 0, GetDistanceToCenter());

        return Mathf.Clamp(normalized, 0.0f, 1.0f);
    }

    public void OnDrawGizmos()
    {
        Debug.Log("draw");
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radiusMax);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _radiusMin);
    }
}
