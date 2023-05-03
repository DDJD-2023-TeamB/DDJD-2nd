using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    private AimComponent _aimComponent;

    private void Awake()
    {
        _aimComponent = GetComponent<AimComponent>();
    }

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
            _aimComponent.GetAimRotation()
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
            _aimComponent.GetAimRotation()
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

    public bool Shoot(GameObject spell, Vector3 direction, bool leaveCaster)
    {
        SkillComponent skillComponent = spell.GetComponent<SkillComponent>();
        if (!skillComponent.CanShoot(direction))
        {
            return false;
        }
        skillComponent.Shoot(direction);

        if (!leaveCaster)
        {
            return true;
        }
        if (_leftSpell == spell)
        {
            _leftSpell = null;
        }
        else if (_rightSpell == spell)
        {
            _rightSpell = null;
        }
        return true;
    }

    private void EndShoot(GameObject spell)
    {
        SkillComponent skillComponent = spell.GetComponent<SkillComponent>();
        skillComponent.DestroySpell();
    }
}
