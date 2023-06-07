using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    [SerializeField]
    private string _floorType = "Wood";

    private SoundEmitter _soundEmitter;

    private FMOD.Studio.PARAMETER_ID _floorTypeParameterId;

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
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            _soundEmitter.Play("footstep");
        }
    }
}
