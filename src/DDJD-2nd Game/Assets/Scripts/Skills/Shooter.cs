using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    private GameObject _leftSpell;
    private GameObject _rightSpell;

    public GameObject LeftSpell
    {
        get { return _leftSpell; }
        set { _leftSpell = value; }
    }
    public GameObject RightSpell
    {
        get { return _rightSpell; }
        set { _rightSpell = value; }
    }

    public GameObject CreateLeftSpell(AimedSkill skill, Transform transform)
    {
        if (_leftSpell != null)
        {
            CancelLeftShoot();
        }
        _leftSpell = GameObject.Instantiate(
            skill.SpellPrefab,
            transform.position,
            Quaternion.identity
        );
        _leftSpell.transform.parent = transform;
        SkillComponent skillComponent = _leftSpell.GetComponent<SkillComponent>();
        skillComponent.SetCaster(gameObject);
        skillComponent.SetSkill(skill);
        return _leftSpell;
    }

    public GameObject CreateRightSpell(AimedSkill skill, Transform transform)
    {
        if (_rightSpell != null)
        {
            CancelRightShoot();
        }
        _rightSpell = GameObject.Instantiate(
            skill.SpellPrefab,
            transform.position,
            Quaternion.identity
        );
        _rightSpell.transform.parent = transform;
        SkillComponent skillComponent = _rightSpell.GetComponent<SkillComponent>();
        skillComponent.SetCaster(gameObject);
        skillComponent.SetSkill(skill);
        return _rightSpell;
    }

    public void CancelLeftShoot()
    {
        if (_leftSpell != null)
        {
            EndShoot(_leftSpell);
            _leftSpell = null;
        }
    }

    public void CancelRightShoot()
    {
        if (_rightSpell != null)
        {
            EndShoot(_rightSpell);
            _rightSpell = null;
        }
    }

    public void CancelShots()
    {
        CancelLeftShoot();
        CancelRightShoot();
    }

    public void Shoot(GameObject spell, Vector3 direction)
    {
        ProjectileComponent skillComponent = spell.GetComponent<ProjectileComponent>();
        skillComponent.Shoot(direction);
        if (_leftSpell == spell)
        {
            _leftSpell = null;
        }
        else if (_rightSpell == spell)
        {
            _rightSpell = null;
        }
    }

    private void EndShoot(GameObject spell)
    {
        Destroy(spell);
    }
}
