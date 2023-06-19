using UnityEngine;

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
    private Element _lightningElement;

    [SerializeField]
    private Element _fireElement;

    [SerializeField]
    private float _runeSpawnDelay = 1.0f;

    private float _hoveringRuneSpawnDelay = 5.0f;

    private int _runeCount = 0;

    private int _hoveringRuneCount = 0;

    private int _chaseRuneCount = 0;

    private int _maxStaticRuneCount = 3;
    private int _maxHoveringRuneCount = 3;
    private int _maxChaseRuneCount = 3;

    public void SpawnRune()
    {
        if (_runeCount < _maxStaticRuneCount)
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
            shooter.StartShoot(this, _fireballSkill);
            _runeCount++;
        }
    }

    public void SpawnHoveringRune()
    {
        if (_hoveringRuneCount < _maxHoveringRuneCount)
        {
            //Get random position in a circle around the boss
            float angle = Random.Range(0.0f, 360.0f);
            float radius = 2.0f;
            Vector3 position = new Vector3(
                transform.position.x + radius * Mathf.Cos(angle),
                transform.position.y + Random.Range(2.0f, 4.0f),
                transform.position.z + radius * Mathf.Sin(angle)
            );

            GameObject rune = Instantiate(_hoveringRune, position, Quaternion.identity);
            HoveringRune shooter = rune.GetComponent<HoveringRune>();
            shooter.StartShoot(this, _fireshotSkill);
            _hoveringRuneCount++;
        }
    }

    public void SpawnChaseRune()
    {
        if (_chaseRuneCount < _maxChaseRuneCount)
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
            shooter.StartShoot(this, _fireshotSkill);
            _chaseRuneCount++;
        }
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

    public int HoveringRuneCount
    {
        get { return _hoveringRuneCount; }
        set { _hoveringRuneCount = value; }
    }

    public GameObject ChaseRunePrefab
    {
        get { return _chaseRunePrefab; }
        set { _chaseRunePrefab = value; }
    }
}
