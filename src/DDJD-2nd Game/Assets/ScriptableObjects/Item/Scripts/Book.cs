using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Book", menuName = "Scriptable Objects/Items/Book")]
public class Book : CollectibleObject
{
    [SerializeField]
    private ItemSkill _itemSkill;

    [SerializeField]
    private FMODUnity.EventReference bookEvent;

    private void Awake() { }

    public override void Use(Player player)
    {
        FMODUnity.RuntimeManager.PlayOneShot(bookEvent, player.transform.position);
        if (!player.PlayerSkills.LearnedSkills.Contains(_itemSkill))
        {
            player.UIController.AddItem(new ItemStack(_itemSkill, 1));
            player.PlayerSkills.LearnedSkills.Add(_itemSkill);
        }
        player.Inventory.RemoveItem(this);
    }
}
