using UnityEngine;

public interface Damageable
{
    void TakeDamage(
        GameObject damager,
        int damage,
        float force,
        Vector3 hitPoint,
        Vector3 hitDirection
    );

    bool IsTriggerDamage();

    // Get the main object that can be damaged, for example, a bone can be damaged, but the main object is the one that suffers the damage


    GameObject GetDamageableObject();
}
