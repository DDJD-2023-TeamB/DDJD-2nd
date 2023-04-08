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
    private SkillStats _stats;
    public virtual SkillStats Stats
    {
        get => _stats;
        set => _stats = value;
    }
}
