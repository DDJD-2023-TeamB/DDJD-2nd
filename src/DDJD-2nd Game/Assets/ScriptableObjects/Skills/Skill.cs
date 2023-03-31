using UnityEngine;
public abstract class Skill : ScriptableObject
{

    [SerializeField]
    private GameObject _spellPrefab;
    public GameObject SpellPrefab { get => _spellPrefab; set => _spellPrefab = value; }

    [SerializeField] 
    private SkillStats _stats;
    public SkillStats Stats { get => _stats; set => _stats = value; }

    [SerializeField]
    private GameObject _handRunePrefab;
    public GameObject HandRunePrefab { get => _handRunePrefab; set => _handRunePrefab = value; }

    private GameObject _rune;

    public GameObject ActivateRune(GameObject parent){
        GameObject rune = GameObject.Instantiate(_handRunePrefab, parent.transform.position, Quaternion.identity);
        rune.transform.parent = parent.transform;   
        rune.transform.rotation = parent.transform.rotation;
        return rune;
    }
}
