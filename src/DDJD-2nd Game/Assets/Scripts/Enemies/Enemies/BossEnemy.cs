using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossEnemy : RangedEnemy
{
    [Header("Boss Enemy Settings")]
    [SerializeField]
    private GameObject _runeAttackPrefab;

    [SerializeField]
    private GameObject _hoveringRune;

    [SerializeField]
    private GameObject _chaseRunePrefab;

    [SerializeField]
    private GameObject _protectionRunePrefab;

    [SerializeField]
    private Skill _fireballSkill;

    [SerializeField]
    private Skill _fireshotSkill;

    [SerializeField]
    private Skill _tornadoSkill;

    [SerializeField]
    private Skill _flamethrowerSkill;

    [SerializeField]
    private Skill _shockwaveSkill;

    [SerializeField]
    private Skill _lightningRay;

    [SerializeField]
    private Skill _boltRay;

    [SerializeField]
    private Element _lightningElement;

    [SerializeField]
    private Element _fireElement;

    [SerializeField]
    private List<Skill> _staticRuneSkills = new List<Skill>();

    [SerializeField]
    private List<Skill> _hoveringRuneSkills = new List<Skill>();

    [SerializeField]
    private List<Skill> _chaseRuneSkills = new List<Skill>();

    [SerializeField]
    private float _runeSpawnDelay = 1.0f;

    private float _hoveringRuneSpawnDelay = 5.0f;

    private int _runeCount = 0;

    private int _maxRuneCount = 9;

    private int _phaseIndex = 4;

    private BossPhase[] _phases = new BossPhase[]
    {
        new BossPhase(0, 3, 1, false, 0.90f),
        new BossPhase(1, 6, 2, false, 0.80f),
        new BossPhase(2, 9, 3, false, 0.60f),
        new BossPhase(3, 12, 4, false, 0.40f),
        new BossPhase(4, 24, 4, false, 0.15f),
    };

    public void AdvancePhase()
    {
        BossPhase phase = _phases[_phaseIndex];
        if (_status.Health / _status.MaxHealth > phase.HealthToAdvance)
        {
            return;
        }

        _phaseIndex++;
        if (_phaseIndex >= _phases.Length)
        {
            _phaseIndex = 0;
        }
        phase = _phases[_phaseIndex];
    }

    public void SpawnRune()
    {
        if (_runeCount >= _maxRuneCount)
        {
            return;
        }
        int maxRuneType = _phases[_phaseIndex].RuneVariety;
        int runeType = Random.Range(0, maxRuneType);
        switch (runeType)
        {
            case 0:
                SpawnStaticRune();
                break;
            case 1:
                SpawnHoveringRune();
                break;
            case 2:
                SpawnChaseRune();
                break;
            case 3:
                SpawnProtectingRune();
                break;
        }
    }

    private void SpawnProtectingRune()
    {
        //Get random position in a circle around the boss
        float angle = Random.Range(0.0f, 360.0f);
        float radius = 2.0f;
        Vector3 position = new Vector3(
            transform.position.x + radius * Mathf.Cos(angle),
            transform.position.y + Random.Range(1.0f, 2.0f),
            transform.position.z + radius * Mathf.Sin(angle)
        );

        GameObject rune = Instantiate(_protectionRunePrefab, position, Quaternion.identity);
        ProtectionRune shooter = rune.GetComponent<ProtectionRune>();

        int index = Random.Range(0, _staticRuneSkills.Count);
        Skill skill = _shockwaveSkill;
        shooter.StartShoot(this, skill);
        _runeCount++;
    }

    private void SpawnStaticRune()
    {
        //Get random position in a circle around the boss
        float angle = Random.Range(0.0f, 360.0f);
        float radius = 5.0f;
        Vector3 position = new Vector3(
            transform.position.x + radius * Mathf.Cos(angle),
            transform.position.y + Random.Range(2.0f, 4.0f),
            transform.position.z + radius * Mathf.Sin(angle)
        );

        GameObject rune = Instantiate(_runeAttackPrefab, position, Quaternion.identity);
        RuneShooter shooter = rune.GetComponent<RuneShooter>();

        int index = Random.Range(0, _staticRuneSkills.Count);
        Skill skill = _staticRuneSkills[index];
        shooter.StartShoot(this, skill);
        _runeCount++;
    }

    public void SpawnHoveringRune()
    {
        //Get random position in a circle around the boss
        float angle = Random.Range(0.0f, 360.0f);
        float radius = 2.0f;
        Vector3 position = new Vector3(
            transform.position.x + radius * Mathf.Cos(angle),
            transform.position.y + Random.Range(2.0f, 4.0f),
            transform.position.z + radius * Mathf.Sin(angle)
        );

        int index = Random.Range(0, _staticRuneSkills.Count);
        Skill skill = _hoveringRuneSkills[index];
        GameObject rune = Instantiate(_hoveringRune, position, Quaternion.identity);
        HoveringRune shooter = rune.GetComponent<HoveringRune>();
        shooter.StartShoot(this, skill);
        _runeCount++;
    }

    public void SpawnChaseRune()
    {
        //Get random position in a circle around the boss
        float angle = Random.Range(0.0f, 360.0f);
        float radius = 2.0f;
        Vector3 position = new Vector3(
            transform.position.x + radius * Mathf.Cos(angle),
            transform.position.y + Random.Range(2.0f, 4.0f),
            transform.position.z + radius * Mathf.Sin(angle)
        );

        GameObject rune = Instantiate(_chaseRunePrefab, position, Quaternion.identity);
        ChaseRune shooter = rune.GetComponent<ChaseRune>();

        int index = Random.Range(0, _chaseRuneSkills.Count);
        Skill skill = _chaseRuneSkills[index];
        shooter.StartShoot(this, skill);
        _runeCount++;
    }

    public override void TakeDamage(
        GameObject damager,
        int damage,
        float force,
        Vector3 hitPoint,
        Vector3 hitDirection,
        Element element
    )
    {
        base.TakeDamage(damager, damage, force, hitPoint, hitDirection, element);
        AdvancePhase();
    }

    public override void Awake()
    {
        base.Awake();
    }

    public GameObject RuneAttackPrefab
    {
        get { return _runeAttackPrefab; }
    }

    public Skill FireballSkill
    {
        get { return _fireballSkill; }
    }

    public Skill TornadoSkill
    {
        get { return _tornadoSkill; }
    }

    public Skill FlamethrowerSkill
    {
        get { return _flamethrowerSkill; }
    }

    public Skill ShockwaveSkill
    {
        get { return _shockwaveSkill; }
    }

    public Skill LightningRay
    {
        get { return _lightningRay; }
    }

    public Element LightningElement
    {
        get { return _lightningElement; }
    }

    public Element FireElement
    {
        get { return _fireElement; }
    }

    public float RuneSpawnDelay
    {
        get { return _runeSpawnDelay; }
    }

    public GameObject HoveringRune
    {
        get { return _hoveringRune; }
    }

    public float HoveringRuneSpawnDelay
    {
        get { return _hoveringRuneSpawnDelay; }
    }

    public int RuneCount
    {
        get { return _runeCount; }
        set { _runeCount = value; }
    }

    public GameObject ChaseRunePrefab
    {
        get { return _chaseRunePrefab; }
        set { _chaseRunePrefab = value; }
    }
}
