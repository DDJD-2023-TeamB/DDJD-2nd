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
            SetRagdollController(_ragdollController);
        }
    }

    public void Start() { }

    public void TakeDamage(
        GameObject damager,
        int damage,
        float force,
        Vector3 hitPoint,
        Vector3 hitDirection,
        Element element
    )
    {
        if (_ragdollController.CanDamage(damager))
        {
            _ragdollController.AddDamageInteraction(damager);
            _damageable?.TakeDamage(damager, damage, force, hitPoint, hitDirection, element);
        }
    }

    //Deprecated probably
    public bool IsTriggerDamage()
    {
        //    return _ragdollController.IsRagdollActive;
        return false;
    }

    public void SetRagdollController(RagdollController ragdollController)
    {
        _ragdollController = ragdollController;
        _damageable = _ragdollController.Damageable;
    }

    public GameObject GetDamageableObject()
    {
        return _ragdollController.gameObject;
    }
}
