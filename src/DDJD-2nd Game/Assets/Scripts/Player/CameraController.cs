using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    private Player _player;

    [SerializeField]
    private CinemachineVirtualCamera _regularCamera;

    [SerializeField]
    private CinemachineVirtualCamera _aimCamera;

    private CinemachineVirtualCamera _activeCamera;

    public CinemachineVirtualCamera ActiveCamera
    {
        get { return _activeCamera; }
    }

    protected void Awake()
    {
        _player = GetComponent<Player>();
        SetAimCamera(false);
    }

    protected void Start() { }

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
}
