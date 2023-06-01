using UnityEngine;

[System.Serializable]
public class HoverSkillStats
{
    [SerializeField]
    private float _upwardForce;

    [SerializeField]
    private float _forwardForce;

    [SerializeField]
    public float _updateRate;

    public float UpwardForce
    {
        get => _upwardForce;
    }

    public float ForwardForce
    {
        get => _forwardForce;
    }

    public float UpdateRate
    {
        get => _updateRate;
    }
}
