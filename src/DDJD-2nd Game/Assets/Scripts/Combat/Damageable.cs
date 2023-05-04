using UnityEngine;

public interface Damageable
{
    void TakeDamage(int damage, Vector3 hitPoint, Vector3 hitDirection);
}