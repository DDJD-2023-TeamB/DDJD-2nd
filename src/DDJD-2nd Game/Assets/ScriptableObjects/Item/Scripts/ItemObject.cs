using MyBox;
using UnityEngine;

public abstract class ItemObject : ScriptableObject
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

    public string Name
    {
        get => _name;
    }

    public Sprite Icon
    {
        get => _icon;
    }
    
}
