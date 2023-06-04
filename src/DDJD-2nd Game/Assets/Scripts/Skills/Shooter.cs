using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    protected AimComponent _aimComponent;
    protected GameObject _leftSpell;
    protected GameObject _rightSpell;
    protected CharacterStatus _status;

    protected virtual void Awake()
    {
        _aimComponent = GetComponent<AimComponent>();
        _status = GetComponent<CharacterStatus>();
    }

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
        _leftSpell = CreateSpell(skill, transform);
        return _leftSpell;
    }

    public GameObject CreateRightSpell(AimedSkill skill, Transform transform)
    {
        if (_rightSpell != null)
        {
            CancelRightShoot();
        }
        _rightSpell = CreateSpell(skill, transform);
        return _rightSpell;
    }

    private GameObject CreateSpell(AimedSkill skill, Transform transform)
    {
        GameObject spell = GameObject.Instantiate(
            skill.SpellPrefab,
            transform.position,
            _aimComponent.GetAimRotation()
        );
        spell.transform.parent = transform;
        SkillComponent skillComponent = spell.GetComponent<SkillComponent>();
        skillComponent.SetCaster(gameObject);
        skillComponent.SetSkill(skill);
        return spell;
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

    public virtual bool Shoot(GameObject spell, Vector3 direction, bool leaveCaster, int manaCost)
    {
        SkillComponent skillComponent = spell.GetComponent<SkillComponent>();
        if (!skillComponent.CanShoot(direction))
        {
            return false;
        }
        if (!_status.ConsumeMana(skillComponent.Skill.Element, manaCost)) // TODO change to skill componeont (canShoot or shoot)
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
