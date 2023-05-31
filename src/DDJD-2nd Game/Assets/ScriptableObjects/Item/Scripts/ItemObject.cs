using MyBox;
using UnityEngine;

public enum ItemType
{
    Potion,
    Book,
    QuestItem, 
    Spell
}
public abstract class ItemObject : ScriptableObject
{
    [SerializeField]
    private string _name;

    [SerializeField]
    private string _description;

    [SerializeField]
    private Sprite _icon;

    protected ItemType _type;

     //[SerializeField]
    //private bool _isStackable;
    //
    //[ConditionalField(nameof(_isStackable))]
    //[SerializeField]
    //private int _maxStack;

    public string Name
    {
        get => _name;
    }

    public Sprite Icon
    {
        get => _icon;
    }

    public ItemType Type
    {
        get => _type;
    }
}
