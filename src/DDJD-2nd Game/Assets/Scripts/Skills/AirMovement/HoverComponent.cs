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

    private Coroutine _stopHoverCoroutine;

    bool _isHovering = false;

    // Start is called before the first frame update
    private void Awake()
    {
        _vfx = GetComponent<VisualEffect>();
        _soundEmitter = GetComponent<SoundEmitter>();
    }

    private void Start()
    {
        _elementParameterId = _soundEmitter.GetParameterId("hover", "Hover Type");
        _vfx.Stop();
    }

    public void Activate()
    {
        _isHovering = true;
        if (_stopHoverCoroutine == null)
        {
            _vfx.Play();
            _soundEmitter.SetParameterWithLabel(
                "hover",
                _elementParameterId,
                _element.SfxDamageLabel,
                true
            );
        }
        else
        {
            StopCoroutine(_stopHoverCoroutine);
            _stopHoverCoroutine = null;
        }
    }

    public void Stop()
    {
        _isHovering = false;
        if (gameObject.activeInHierarchy)
        {
            _vfx.Stop();
            _soundEmitter.Stop("hover");
        }
    }

    public void SetVelocity(Vector3 velocity)
    {
        _vfx.SetVector3("Velocity", velocity);
    }

    // Update is called once per frame
    void Update() { }

    private IEnumerator StopHover()
    {
        yield return null;
        if (!_isHovering)
        {
            _vfx.Stop();
            _soundEmitter.Stop("hover");
        }
        _stopHoverCoroutine = null;
    }

    public Element Element
    {
        get { return _element; }
        set { _element = value; }
    }
}
