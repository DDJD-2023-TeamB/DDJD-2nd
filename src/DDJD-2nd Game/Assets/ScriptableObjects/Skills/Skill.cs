using UnityEngine;

public abstract class Skill : ScriptableObject
{
    [SerializeField]
    private GameObject _spellPrefab;
    public GameObject SpellPrefab
    {
        get => _spellPrefab;
        set => _spellPrefab = value;
    }

    [SerializeField]
    private Element _element;

    [SerializeField]
    private SkillStats _stats;
    public virtual SkillStats SkillStats
    {
        get => _stats;
        set => _stats = value;
    }

    public Element Element
    {
        get => _element;
    }
}
