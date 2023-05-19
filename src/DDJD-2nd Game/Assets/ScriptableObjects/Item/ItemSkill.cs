using UnityEngine;

[CreateAssetMenu(fileName = "ItemSkill", menuName = "Items/ItemSkill", order = 1)]
public class ItemSkill : Item
{
    [SerializeField]
    private AimedSkill _skill;
    public AimedSkill Skill
    {
        get => _skill;
        set => _skill = value;
    }
}
