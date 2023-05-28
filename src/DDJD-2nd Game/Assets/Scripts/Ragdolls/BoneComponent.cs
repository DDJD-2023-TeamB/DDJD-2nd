using UnityEngine;

public class BoneComponent : MonoBehaviour, Damageable
{
    //RagdollController can is set by the RagdollController itself
    [SerializeField]
    private RagdollController _ragdollController;

    private Damageable _damageable;

    public void Awake()
    {
        if (_ragdollController != null)
        {
            _damageable = _ragdollController.GetComponent<Damageable>();
        }
    }

    public void Start() { }

    public void TakeDamage(
        GameObject damager,
        int damage,
        float force,
        Vector3 hitPoint,
        Vector3 hitDirection
    )
    {
        if (_ragdollController.CanDamage(damager))
        {
            _damageable?.TakeDamage(damager, damage, force, hitPoint, hitDirection);
        }
    }

    public bool IsTriggerDamage()
    {
        //    return _ragdollController.IsRagdollActive;
        return false;
    }

    public void SetRagdollController(RagdollController ragdollController)
    {
        _ragdollController = ragdollController;
        _damageable = _ragdollController.GetComponent<Damageable>();
    }

    public GameObject GetDamageableObject()
    {
        return _ragdollController.gameObject;
    }
}
