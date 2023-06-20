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
    private float _maxAngle = 75.0f;

    [SerializeField]
    private float _minAngle = -45.0f;

    [SerializeField]
    private float _runFov = 60.0f;

    [SerializeField]
    private float _walkFov = 50.0f;
    public CinemachineVirtualCamera ActiveCamera
    {
        get { return _activeCamera; }
    }

    private FovController _fovController;

    protected void Awake()
    {
        _player = GetComponent<Player>();
        SetAimCamera(false);
    }

    protected void Start()
    {
        ResetCameraRotation();
    }

    public void ChangeFov(float value, float time)
    {
        if (_fovController != null)
        {
            _fovController.ChangeFov(value, 0.1f);
        }
    }

    public void ChangeFov(float value)
    {
        _fovController.ChangeFovInstant(value);
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
        _fovController = _activeCamera.GetComponent<FovController>();
    }

    public void RotateCamera(Vector3 lookInput, bool moveRigidbody)
    {
        Vector3 cameraRotation = _cameraTarget.transform.localEulerAngles;
        float yRotation = 0,
            xRotation = 0;
        if (moveRigidbody)
        {
            cameraRotation.y = transform.localEulerAngles.y;
        }
        if (Math.Abs(lookInput.y) > 0.5)
        {
            yRotation = -lookInput.y * _cameraRotationSpeed * Time.deltaTime;
            cameraRotation.x += yRotation;
        }

        if (Math.Abs(lookInput.x) > 0.5)
        {
            xRotation = lookInput.x * _cameraRotationSpeed * Time.deltaTime;
            cameraRotation.y += xRotation;
        }

        //TODO:: Limits could be improved
        if (cameraRotation.x > 180)
        {
            cameraRotation.x -= 360;
        }
        cameraRotation.x = Mathf.Clamp(cameraRotation.x, _minAngle, _maxAngle);
        if (moveRigidbody)
        {
            transform.localEulerAngles = new Vector3(0f, cameraRotation.y, 0f);
            //_player.Rigidbody.transform.Rotate(new Vector3(0f, xRotation, 0f), Space.Self);
            _cameraTarget.transform.localEulerAngles = new Vector3(
                cameraRotation.x,
                _cameraTarget.transform.localEulerAngles.y,
                _cameraTarget.transform.localEulerAngles.z
            );
        }
        else
        {
            _cameraTarget.transform.localEulerAngles = cameraRotation;
        }

        //Fix broken camera
    }

    public void ResetCameraRotation()
    {
        _cameraTarget.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
    }

    public void setCameraRotationSpeed(float value)
    {
        _cameraRotationSpeed = value;
    }

    public GameObject CameraTarget
    {
        get { return _cameraTarget; }
    }

    public CinemachineVirtualCamera AimCamera
    {
        get { return _aimCamera; }
    }

    public float RunFov
    {
        get { return _runFov; }
    }

    public float WalkFov
    {
        get { return _walkFov; }
    }
}
