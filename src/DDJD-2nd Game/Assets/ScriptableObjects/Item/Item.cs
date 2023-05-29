using MyBox;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    [SerializeField]
    private string _name;

    [SerializeField]
    private string _description;

    [SerializeField]
    private Sprite _icon;


    //[SerializeField]
    //private bool _isStackable;
    //
    //[ConditionalField(nameof(_isStackable))]
    //[SerializeField]
    //private int _maxStack;

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Item otherItem = (Item)obj;
        return _name == otherItem._name;
    }

    public string Name
    {
        get => _name;
    }

    public Sprite Icon
    {
        get => _icon;
    }
}
