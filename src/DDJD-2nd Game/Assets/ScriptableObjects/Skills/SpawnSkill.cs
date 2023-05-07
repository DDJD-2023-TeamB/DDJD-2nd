using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawn", menuName = "Scriptable Objects/Skills/Spawn", order = 2)]
public class SpawnSkill : AimedSkill
{
    [SerializeField]
    private SpawnSkillStats _stats;

    // Unity doesn't support covariant return types
    public override SkillStats SkillStats
    {
        get { return (SkillStats)_stats; }
        set { _stats = (SpawnSkillStats)value; }
    }
    public SpawnSkillStats SpawnStats
    {
        get { return _stats; }
    }
}
