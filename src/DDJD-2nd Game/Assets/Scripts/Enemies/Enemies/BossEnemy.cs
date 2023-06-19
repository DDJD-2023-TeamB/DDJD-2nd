using UnityEngine;

public class BossEnemy : RangedEnemy
{
    [Header("Boss Enemy Settings")]
    [SerializeField]
    private GameObject _runeAttackPrefab;

    [SerializeField]
    private Skill _fireballSkill;

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

    private float _runeSpawnDelay = 10.0f;

    private int _runeCount = 0;

    public void SpawnRune()
    {
        if (_runeCount < 3)
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
            shooter.StartShoot(gameObject, _fireballSkill);
            _runeCount++;
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
}
