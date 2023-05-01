using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillComponent : MonoBehaviour
{
    protected GameObject _caster;

    [SerializeField]
    private bool _damageCaster = false;

    public void SetCaster(GameObject caster)
    {
        _caster = caster;
    }

    public abstract void SetSkill(Skill skill);

    protected void Damage(GameObject target, int damage, Vector3 hitPoint, Vector3 direction)
    {
        Damageable damageable = target.GetComponent<Damageable>();
        if (!_damageCaster && target == _caster)
        {
            return;
        }
        if (damageable == null)
        {
            return;
        }

        damageable.TakeDamage(damage, hitPoint, direction);
    }
}
