using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class HoverComponent : MonoBehaviour
{
    private VisualEffect _vfx;
    private SoundEmitter _soundEmitter;
    private FMOD.Studio.PARAMETER_ID _elementParameterId;

    private Element _element;

    // Start is called before the first frame update
    private void Awake()
    {
        _vfx = GetComponent<VisualEffect>();
        _soundEmitter = GetComponent<SoundEmitter>();
    }

    private void Start()
    {
        _elementParameterId = _soundEmitter.GetParameterId("hover", "Hover Type");
        _soundEmitter.SetParameterWithLabel(
            "hover",
            _elementParameterId,
            _element.SfxDamageLabel,
            true
        );
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

    public Element Element
    {
        get { return _element; }
        set { _element = value; }
    }
}
