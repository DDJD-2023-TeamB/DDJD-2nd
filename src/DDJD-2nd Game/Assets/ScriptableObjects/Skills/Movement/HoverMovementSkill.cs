using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "HoverMovementSkill",
    menuName = "Scriptable Objects/Skills/Movement/HoverMovementSkill",
    order = 1
)]
public class HoverMovementSkill : AirMovementSkill
{
    [SerializeField]
    private HoverSkillStats _hoverSkillStats;
    public HoverSkillStats HoverSkillStats
    {
        get => _hoverSkillStats;
    }

    public override AirMovementComponent Initialize(GameObject obj)
    {
        HoverMovementComponent hoverComponent = obj.GetComponent<HoverMovementComponent>();
        if (hoverComponent == null)
        {
            hoverComponent = obj.AddComponent<HoverMovementComponent>();
        }
        hoverComponent.SetSkill(this);
        return hoverComponent;
    }
}
