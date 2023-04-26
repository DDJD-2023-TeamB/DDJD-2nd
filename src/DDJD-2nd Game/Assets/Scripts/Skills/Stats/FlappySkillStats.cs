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

    [SerializeField]
    private int _maxJumps = 5;

    public int MaxJumps
    {
        get => _maxJumps;
    }

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
