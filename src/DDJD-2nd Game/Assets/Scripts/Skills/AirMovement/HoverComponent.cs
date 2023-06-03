using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class HoverComponent : MonoBehaviour
{
    private VisualEffect _vfx;
    private SoundEmitter _soundEmitter;

    // Start is called before the first frame update
    private void Awake()
    {
        _vfx = GetComponent<VisualEffect>();
        _soundEmitter = GetComponent<SoundEmitter>();
    }

    public void Stop()
    {
        _vfx.Stop();
        _soundEmitter.Stop("hover");
    }

    public void SetVelocity(Vector3 velocity)
    {
        _vfx.SetVector3("Velocity", velocity);
    }

    // Update is called once per frame
    void Update() { }
}
