using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AimedSkill : Skill
{
    [SerializeField]
    private GameObject _handRunePrefab;
    public GameObject HandRunePrefab
    {
        get => _handRunePrefab;
        set => _handRunePrefab = value;
    }

    public GameObject ActivateRune(GameObject parent)
    {
        GameObject rune = GameObject.Instantiate(
            _handRunePrefab,
            parent.transform.position,
            Quaternion.identity
        );
        rune.transform.parent = parent.transform;
        rune.transform.rotation = parent.transform.rotation;
        return rune;
    }
}
