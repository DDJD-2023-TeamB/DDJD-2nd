using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class RuneShooter : MonoBehaviour
{
    protected BossEnemy _caster;
    protected Skill _skill;

    protected float _minShootCooldown = 0.5f;
    protected float _maxShootCooldown = 3f;

    protected float _duration = 30.0f;

    protected SkillComponent _skillComponent;

    protected Player _player;

    protected Rigidbody _rigidbody;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Vector3 direction = (_player.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    protected float GetCooldown()
    {
        return Random.Range(_minShootCooldown, _maxShootCooldown);
    }

    public virtual void StartShoot(BossEnemy caster, Skill skill)
    {
        _caster = caster;
        _skill = skill;
        StartCoroutine(Shoot());
    }

    protected IEnumerator Stop()
    {
        yield return new WaitForSeconds(_duration);
        Destroy(gameObject);
    }

    protected void CreateSpell()
    {
        GameObject spell = GameObject.Instantiate(
            _skill.SpellPrefab,
            transform.position,
            transform.rotation
        );
        spell.transform.parent = transform;
        _skillComponent = spell.GetComponent<SkillComponent>();
        _skillComponent.SetCaster(_caster.gameObject);
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

    protected IEnumerator Shoot()
    {
        yield return new WaitForSeconds(GetCooldown());
        CreateSpell();
    }

    protected void ShootAtPlayer()
    {
        if (_player != null)
        {
            Vector3 direction = (_player.transform.position - transform.position).normalized;

            _skillComponent.Shoot(direction);
        }
        StartCoroutine(Shoot());
    }

    public void OnDestroy()
    {
        if (_skillComponent != null)
        {
            _skillComponent.ChargeComponent.OnChargeComplete -= ShootAtPlayer;
        }

        _caster.RuneCount--;
    }
}
