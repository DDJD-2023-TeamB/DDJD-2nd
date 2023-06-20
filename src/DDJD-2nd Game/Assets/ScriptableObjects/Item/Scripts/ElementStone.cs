using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ElementStone", menuName = "Scriptable Objects/Items/ElementStone")]
public class ElementStone : CollectibleObject
{
    [SerializeField]
    private Element _element;

    [SerializeField]
    private FMODUnity.EventReference _event;

    [SerializeField]
    private string _sfxParameterValue;

    private void Awake() { }

    public override void Use(Player player)
    {
        SoundEmitter.PlayOneShot(_event, "Element Overlay", _sfxParameterValue);
        if (!player.PlayerSkills.Elements.Contains(_element))
        {
            player.PlayerSkills.Elements.Add(_element);
        }
        player.Inventory.RemoveItem(this);
    }
}
