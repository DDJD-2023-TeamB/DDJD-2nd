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

    public string Name
    {
        get => _name;
    }

    public Sprite Icon
    {
        get => _icon;
    }

    public string Description
    {
        get => _description;
    }
}
