using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Book", menuName = "Scriptable Objects/Items/Book")]
public class Book : CollectibleObject
{
    [SerializeField]
    private ItemSkill _itemSkill;

    private Player _player;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public override void Use(InventoryUI inventoryUI)
    {
        if (!_player.PlayerSkills.LearnedSkills.Contains(_itemSkill))
        {
            _player.PlayerSkills.LearnedSkills.Add(_itemSkill);
            bool res = inventoryUI.AddItem(new ItemStack(_itemSkill, 1));
        }
        _player.Inventory.RemoveItem(this);
    }
}
