using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class Ambience : MonoBehaviour
{
    protected SoundEmitter _soundEmitter;

    [SerializeField]
    private Transform _center;

    [SerializeField]
    [Tooltip("The radius of the circle around the center where the sound is maximum")]
    private float _radiusMax = 30.0f;

    [SerializeField]
    [Tooltip("The radius of the circle around the center where the is starting to change")]
    private float _radiusMin = 75.0f;

    [SerializeField]
    private Transform _upperRightCorner;

    [SerializeField]
    private Transform _lowerLeftCorner;

    protected bool _isPlayerInRange = false;

    protected Player _player;

    protected void Awake()
    {
        _soundEmitter = GetComponent<SoundEmitter>();
    }

    protected virtual void Start()
    {
        //FInd by tag
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
                _soundEmitter.Play("ambience");
                _isPlayerInRange = true;
            }
            else if (!inRange && _isPlayerInRange)
            {
                _soundEmitter.Stop("ambience");
                _isPlayerInRange = false;
            }
        }
    }

    protected bool IsPlayerInRange()
    {
        return _player.transform.position.x >= _lowerLeftCorner.position.x
            && _player.transform.position.x <= _upperRightCorner.position.x
            && _player.transform.position.z >= _lowerLeftCorner.position.z
            && _player.transform.position.z <= _upperRightCorner.position.z;
    }

    protected float GetDistanceToCenter()
    {
        return Vector3.Distance(_player.transform.position, _center.position);
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

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_center.position, _radiusMax);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_center.position, _radiusMin);

        //Draw rectangle
        Gizmos.color = Color.green;
        Gizmos.DrawLine(
            _upperRightCorner.position,
            new Vector3(
                _upperRightCorner.position.x,
                _upperRightCorner.position.y,
                _lowerLeftCorner.position.z
            )
        );
        Gizmos.DrawLine(
            _upperRightCorner.position,
            new Vector3(
                _lowerLeftCorner.position.x,
                _upperRightCorner.position.y,
                _upperRightCorner.position.z
            )
        );
        Gizmos.DrawLine(
            _lowerLeftCorner.position,
            new Vector3(
                _lowerLeftCorner.position.x,
                _lowerLeftCorner.position.y,
                _upperRightCorner.position.z
            )
        );
        Gizmos.DrawLine(
            _lowerLeftCorner.position,
            new Vector3(
                _upperRightCorner.position.x,
                _lowerLeftCorner.position.y,
                _lowerLeftCorner.position.z
            )
        );
    }
}
