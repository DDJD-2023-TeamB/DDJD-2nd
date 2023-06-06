using UnityEngine;
using Cinemachine;
using System.Collections;
using System;

public class CameraController : MonoBehaviour
{
    private Player _player;

    [SerializeField]
    private CinemachineVirtualCamera _regularCamera;

    [SerializeField]
    private CinemachineVirtualCamera _aimCamera;

    private CinemachineVirtualCamera _activeCamera;

    [SerializeField]
    private GameObject _cameraTarget;

    [SerializeField]
    private float _cameraRotationSpeed = 20f;

    [SerializeField]
    private float _minAngle = 30f;

    [SerializeField]
    private float _maxAngle = 330f;
    public CinemachineVirtualCamera ActiveCamera
    {
        get { return _activeCamera; }
    }

    protected void Awake()
    {
        _player = GetComponent<Player>();
        SetAimCamera(false);
    }

    protected void Start()
    {
        ResetCameraRotation();
    }

    public void ShakeCamera(float intensity, float time)
    {
        _activeCamera
            .GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>()
            .m_AmplitudeGain = intensity;
        StartCoroutine(StopShake(time, _activeCamera));
    }

    private IEnumerator StopShake(float time, CinemachineVirtualCamera camera)
    {
        yield return new WaitForSeconds(time);
        camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
    }

    public void SetAimCamera(bool aim)
    {
        if (aim)
        {
            _aimCamera.Priority = 15;
            _activeCamera = _aimCamera;
        }
        else
        {
            _aimCamera.Priority = 0;
            _activeCamera = _regularCamera;
        }
    }

    public void RotateCamera(Vector3 lookInput, bool moveRigidbody)
    {
        Vector3 cameraRotation = _cameraTarget.transform.localEulerAngles;
        float minAngle = _minAngle;
        float maxAngle = _maxAngle;
        if (Math.Abs(lookInput.y) > 0.5)
        {
            float amountToRotate = -lookInput.y * _cameraRotationSpeed * Time.deltaTime;
            float finalAngle = _cameraTarget.transform.localEulerAngles.x + amountToRotate;
            if (finalAngle <= minAngle || finalAngle >= maxAngle)
            {
                cameraRotation.x += amountToRotate;
            }
        }

        if (Math.Abs(lookInput.x) > 0.5)
        {
            float amountToRotate = lookInput.x * _cameraRotationSpeed * Time.deltaTime;
            if (moveRigidbody)
            {
                _player.Rigidbody.MoveRotation(
                    _player.Rigidbody.rotation
                        * Quaternion.Euler(new Vector3(0f, amountToRotate, 0f))
                );
            }
            else
            {
                cameraRotation.y += amountToRotate;
            }
        }

        //Fix camera angle if broken
        //float cameraAngle = _player.CameraTarget.transform.localEulerAngles.x;
        //if (cameraAngle > minAngle && cameraAngle < maxAngle)
        //{
        //    float amountToRotate = minAngle - cameraAngle;
        //    _player.CameraTarget.transform.Rotate(
        //        new Vector3(amountToRotate, amountToRotate, 0f),
        //        Space.Self
        //    );
        //}

        _cameraTarget.transform.localEulerAngles = cameraRotation;
    }

    public void ResetCameraRotation()
    {
        _cameraTarget.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
    }

    public GameObject CameraTarget
    {
        get { return _cameraTarget; }
    }

    public CinemachineVirtualCamera AimCamera
    {
        get { return _aimCamera; }
    }
}
