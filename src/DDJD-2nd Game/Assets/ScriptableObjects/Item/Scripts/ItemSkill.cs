using UnityEngine;

[CreateAssetMenu(
    fileName = "ItemSkill",
    menuName = "Scriptable Objects/Items/ItemSkill",
    order = 1
)]
public class ItemSkill : ItemObject
{
    [SerializeField]
    private AimedSkill _skill;
    public AimedSkill Skill
    {
        get => _skill;
        set => _skill = value;
    }
}
