using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDamageable : MonoBehaviour, Damageable
{
    protected Rigidbody _rb;
    protected CharacterStatus _status;

    [SerializeField]
    protected GameObject _deathVFX;

    private RagdollController _ragdollController;

    public void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _status = GetComponent<CharacterStatus>();
        _ragdollController = GetComponent<RagdollController>();
    }

    public void Start()
    {
        _status.OnDeath += Die;
        _ragdollController?.DeactivateRagdoll();
    }

    // public void Update()
    // {
    //     if (transform.position.y == 100)
    //     {
    //         Die(0, Vector3.zero, Vector3.zero);
    //     }
    // }

    public void Die(int force, Vector3 hitPoint, Vector3 hitDirection)
    {
        if (_ragdollController == null)
        {
            SpawnDeathVFX(transform.position);
            return;
        }
        _ragdollController.ActivateRagdoll();
        _ragdollController.PushRagdoll(force, hitPoint, hitDirection);
        StartCoroutine(WaitAndDie(3f));
    }

    private GameObject SpawnDeathVFX(Vector3 position) // TODO SpawnDeathVFX being called!????????????????????????????
    {
        GameObject deathVFX = Instantiate(_deathVFX, position, transform.rotation);
        Destroy(gameObject, 0.1f);
        Destroy(deathVFX, 3f);
        return deathVFX;
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
        _status.TakeDamage(damager, damage, hitPoint, hitDirection);
    }

    public GameObject GetDamageableObject()
    {
        return gameObject;
    }

    public bool IsTriggerDamage()
    {
        return false;
    }

    public Rigidbody Rigidbody
    {
        get { return _rb; }
    }

    protected IEnumerator WaitAndDie(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Transform ragdollTransform = _ragdollController.GetRagdollTransform();
        SpawnDeathVFX(ragdollTransform.position);
    }
}
