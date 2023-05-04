using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "FlappyMovementSkill",
    menuName = "Scriptable Objects/Skills/Movement/FlappyMovementSkill",
    order = 1
)]
public class FlappyMovementSkill : AirMovementSkill
{
    [SerializeField]
    private FlappySkillStats _flappySkillStats;
    public FlappySkillStats FlappySkillStats
    {
        get => _flappySkillStats;
    }

    public override AirMovementComponent Initialize(GameObject obj)
    {
        FlappyMovementComponent flappyComponent = obj.GetComponent<FlappyMovementComponent>();
        if (flappyComponent == null)
        {
            flappyComponent = obj.AddComponent<FlappyMovementComponent>();
        }
        flappyComponent.SetSkill(this);
        return flappyComponent;
    }
}
