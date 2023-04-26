using UnityEngine;

[System.Serializable]
public class FlappySkillStats
{
    [SerializeField]
    private float _upwardForce;

    [SerializeField]
    private float _forwardForce;

    [SerializeField]
    public float _cooldown;

    public float UpwardForce
    {
        get => _upwardForce;
    }

    public float ForwardForce
    {
        get => _forwardForce;
    }

    public float Cooldown
    {
        get => _cooldown;
    }
}
