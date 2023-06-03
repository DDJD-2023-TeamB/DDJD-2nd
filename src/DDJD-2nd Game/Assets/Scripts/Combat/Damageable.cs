using UnityEngine;

public interface Damageable
{
    void TakeDamage(int damage, float force, Vector3 hitPoint, Vector3 hitDirection);

    bool IsTriggerDamage();
}
