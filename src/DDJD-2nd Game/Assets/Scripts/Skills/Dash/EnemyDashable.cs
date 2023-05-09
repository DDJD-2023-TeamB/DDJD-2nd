using UnityEngine;

public class EnemyDashable : Dashable
{
    protected override Vector3 GetDashDirection()
    {
        return Vector3.zero;
    }

    protected override void SetDashAnimation() { }
}
