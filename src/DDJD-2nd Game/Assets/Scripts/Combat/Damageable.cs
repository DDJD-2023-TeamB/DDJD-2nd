using UnityEngine;

public interface Damageable
{
    //Returns true if it's a true collision (For example, if a normal projectile should be destroyed)
    void TakeDamage(int damage, float force, Vector3 hitPoint, Vector3 hitDirection);

    bool IsTriggerDamage();
}
