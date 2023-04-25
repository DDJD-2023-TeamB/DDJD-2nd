using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FovController : MonoBehaviour
{
    private CinemachineVirtualCamera _camera;

    private void Awake()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ChangeFov(float fov, float time = 0.25f)
    {
        StopAllCoroutines();
        StartCoroutine(ChangeFovCoroutine(fov, time));
    }

    IEnumerator ChangeFovCoroutine(float fov, float time)
    {
        float timeTaken = 0;
        float startValue = _camera.m_Lens.FieldOfView;

        while (timeTaken < time)
        {
            timeTaken += Time.deltaTime;
            _camera.m_Lens.FieldOfView = Mathf.Lerp(startValue, fov, timeTaken / time);
            yield return null;
        }

        _camera.m_Lens.FieldOfView = fov;
    }
}
