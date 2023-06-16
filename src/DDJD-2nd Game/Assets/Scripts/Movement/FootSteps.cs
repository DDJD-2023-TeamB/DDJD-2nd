using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    [SerializeField]
    private string _floorType = "Wood";

    private SoundEmitter _soundEmitter;

    private FMOD.Studio.PARAMETER_ID _floorTypeParameterId;

    private bool _skipNextStep = false;

    protected void Awake()
    {
        _soundEmitter = GetComponent<SoundEmitter>();
    }

    protected void Start()
    {
        _soundEmitter.SetParameterWithLabel("footsteps", _floorTypeParameterId, _floorType, false);
    }

    public void ChangeFloorType(string type)
    {
        _floorType = type;
    }

    // Update is called once per frame
    void Update() { }

    public void PlayFootstep(AnimationEvent animationEvent)
    {
        if (_skipNextStep)
        {
            _skipNextStep = false;
            return;
        }
        float weight = animationEvent.animatorClipInfo.weight;
        if (weight >= 0.48f)
        {
            Play();
            _skipNextStep = weight <= 0.5f && weight >= 0.48f;
        }
    }

    public void Play()
    {
        _soundEmitter.Play("footstep");
    }
}
