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
    private HoverMovementComponent _hoverComponent;

    public override AirMovementComponent Initialize(GameObject obj)
    {
        HoverMovementComponent hoverComponent = obj.GetComponent<HoverMovementComponent>();
        if (_hoverComponent == null)
        {
            _hoverComponent = obj.AddComponent<HoverMovementComponent>();
        }
        _hoverComponent.SetSkill(this);
        return _hoverComponent;
    }
}
