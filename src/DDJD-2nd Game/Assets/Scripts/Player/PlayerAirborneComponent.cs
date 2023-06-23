using UnityEngine;
using Unity.Mathematics;

public class PlayerAirborneComponent : MonoBehaviour
{
    protected Player _player;
    protected Rigidbody _rb;

    private bool _isActive = false;

    private FMOD.Studio.PARAMETER_ID _sfxIntensityID;

    private SoundEmitter _soundEmitter;

    protected virtual void Awake()
    {
        _player = GetComponent<Player>();
        _rb = GetComponent<Rigidbody>();
        _soundEmitter = GetComponent<SoundEmitter>();
    }

    public void Start()
    {
        _soundEmitter = _player.SoundEmitter;
        _sfxIntensityID = _soundEmitter.GetParameterId("air", "Hover intensity");
    }

    public virtual void Reset() { }

    public virtual void SetSkill(AirMovementSkill skill)
    {
        enabled = true;
    }

    public void StartAirborne()
    {
        _isActive = true;
        Debug.Log("Start airbone");
        _soundEmitter?.SetParameter("air", _sfxIntensityID, 0.0f);
        _soundEmitter?.Play("air");
    }

    public void StopAirborne()
    {
        _isActive = false;
        _soundEmitter.Stop("air");
    }

    public virtual void Update()
    {
        if (_isActive)
        {
            float intensity = _player.Rigidbody.velocity.magnitude / 20.0f;
            intensity = Mathf.Clamp(0.0f, 1.0f, intensity);

            _soundEmitter.SetParameter("air", _sfxIntensityID, intensity);
        }
    }
}
