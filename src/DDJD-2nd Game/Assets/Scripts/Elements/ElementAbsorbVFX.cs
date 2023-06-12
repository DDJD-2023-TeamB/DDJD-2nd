using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ElementAbsorbVFX : MonoBehaviour
{
    [SerializeField]
    private GameObject targetVFX;
    private VisualEffect _vfx;

    private void Awake()
    {
        _vfx = GetComponent<VisualEffect>();
    }

    public void SetTarget(Transform parent)
    {
        targetVFX.transform.parent = parent;
        targetVFX.transform.localPosition = Vector3.zero;
    }

    public void Stop()
    {
        _vfx.Stop();
        Destroy(_vfx.gameObject, 1f);
    }
}
