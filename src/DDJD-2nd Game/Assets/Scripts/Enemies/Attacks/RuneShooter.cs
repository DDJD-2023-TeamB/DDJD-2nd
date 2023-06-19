using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneShooter : MonoBehaviour
{
    private GameObject _caster;
    private Skill _skill;

    private float _minShootCooldown = 0.5f;
    private float _maxShootCooldown = 3f;

    private float _duration = 30.0f;

    private SkillComponent _skillComponent;

    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (_player.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    private float GetCooldown()
    {
        return Random.Range(_minShootCooldown, _maxShootCooldown);
    }

    public void StartShoot(GameObject caster, Skill skill)
    {
        _caster = caster;
        _skill = skill;
        StartCoroutine(Shoot());
    }

    private IEnumerator Stop()
    {
        yield return new WaitForSeconds(_duration);
        Destroy(gameObject);
    }

    private void CreateSpell()
    {
        GameObject spell = GameObject.Instantiate(
            _skill.SpellPrefab,
            transform.position,
            transform.rotation
        );
        spell.transform.parent = transform;
        _skillComponent = spell.GetComponent<SkillComponent>();
        _skillComponent.SetCaster(_caster);
        _skillComponent.SetSkill(_skill);
        if (_skill.SkillStats.CastType == CastType.Charge)
        {
            _skillComponent.ChargeComponent.OnChargeComplete += ShootAtPlayer;
        }
        else if (_skill.SkillStats.CastType == CastType.Instant)
        {
            ShootAtPlayer();
        }
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            CreateSpell();
            yield return new WaitForSeconds(GetCooldown());
        }
    }

    private void ShootAtPlayer()
    {
        if (_player != null)
        {
            Vector3 direction = (_player.transform.position - transform.position).normalized;

            _skillComponent.Shoot(direction);
        }
    }
}
