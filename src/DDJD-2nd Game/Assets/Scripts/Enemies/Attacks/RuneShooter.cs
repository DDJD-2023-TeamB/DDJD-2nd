using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class RuneShooter : MonoBehaviour, Damageable, NonPushable
{
    [SerializeField]
    protected Transform _spellOrigin;
    protected BossEnemy _caster;
    protected Skill _skill;

    protected float _minShootCooldown = 0.5f;
    protected float _maxShootCooldown = 3f;

    protected float _duration = 30.0f;

    protected SkillComponent _skillComponent;

    protected Player _player;

    protected Rigidbody _rigidbody;

    [SerializeField]
    private float _health = 50.0f;

    private static int _triggerDamageHash = Animator.StringToHash("TakeDamage");

    private Animator _animator;

    [SerializeField]
    private GameObject _deathVfxPrefab;

    private float _runeSize = 1.0f;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        UpdateRotation();
    }

    protected virtual void UpdateRotation()
    {
        Vector3 direction = (_player.transform.position - transform.position).normalized;

        // slowly rotate towards the player
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(direction),
            Time.deltaTime * 5f
        );
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
        StartCoroutine(Stop());
    }

    protected IEnumerator Stop()
    {
        yield return new WaitForSeconds(_duration);
        Die();
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
        else if (_skill.SkillStats.CastType == CastType.Hold)
        {
            ShootAtPlayer();
        }
    }

    protected virtual IEnumerator Shoot()
    {
        yield return new WaitForSeconds(GetCooldown());
        CreateSpell();
    }

    protected void ShootAtPlayer()
    {
        if (_player != null)
        {
            Vector3 direction = GetPlayerDirection();

            _skillComponent.Shoot(direction);
        }
        if (_skill.SkillStats.CastType != CastType.Hold)
        {
            StartCoroutine(Shoot());
        }
    }

    public void OnDestroy()
    {
        _caster.RuneCount--;
    }

    public void TakeDamage(
        GameObject damager,
        int damage,
        float force,
        Vector3 hitPoint,
        Vector3 hitDirection,
        Element element
    )
    {
        _animator.SetTrigger(_triggerDamageHash);
        _health -= damage;
        if (_health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (gameObject == null)
        {
            return;
        }
        GameObject deathVfx = Instantiate(_deathVfxPrefab, transform.position, transform.rotation);
        VisualEffect vfx = deathVfx.GetComponent<VisualEffect>();
        vfx.SetFloat("Size", _runeSize);

        Destroy(deathVfx, 1.5f);
        Destroy(gameObject);
    }

    public bool IsTriggerDamage()
    {
        return false;
    }

    // Get the main object that can be damaged, for example, a bone can be damaged, but the main object is the one that suffers the damage


    public GameObject GetDamageableObject()
    {
        return gameObject;
    }

    protected Vector3 GetPlayerDirection()
    {
        Vector3 targetPosition = _player.transform.position;

        float distanceToCaster =
            Vector3.Distance(transform.position, _player.transform.position) * 0.1f;
        // Apply offset
        targetPosition +=
            new Vector3(
                Random.Range(-0.1f, 0.5f),
                Random.Range(0.2f, 2.0f),
                Random.Range(-0.5f, 0.5f)
            ) * distanceToCaster;

        Vector3 direction = (targetPosition - transform.position).normalized;

        return direction;
    }
}
