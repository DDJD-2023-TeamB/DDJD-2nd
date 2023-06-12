using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Book", menuName = "Scriptable Objects/Items/Book")]
public class Book : CollectibleObject
{
    [SerializeField]
    private ItemSkill _itemSkill;

    private void Awake() { }

    public override void Use(Player player)
    {
        if (!player.PlayerSkills.LearnedSkills.Contains(_itemSkill))
        {
            player.UIController.AddItem(new ItemStack(_itemSkill, 1));
            player.PlayerSkills.LearnedSkills.Add(_itemSkill);
        }
        player.Inventory.RemoveItem(this);
    }
}
