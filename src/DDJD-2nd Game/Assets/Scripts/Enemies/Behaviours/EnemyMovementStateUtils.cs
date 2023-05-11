using UnityEngine;
using System.Collections;

public class EnemyMovementStateUtils
{
    public static IEnumerator DashCoroutine(BasicEnemy enemy, float minDistance, GenericState state)
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            bool canDash =
                enemy.NavMeshAgent.remainingDistance > minDistance
                && enemy.EnemyDashable.IsDashReady();
            if (canDash)
            {
                DashToPosition(enemy, enemy.NavMeshAgent.destination, state);
            }
        }
    }

    public static void DashToPosition(BasicEnemy enemy, Vector3 position, GenericState state)
    {
        Vector3 direction = (position - enemy.transform.position).normalized;
        if (direction.y < 0)
        {
            direction.y = 0;
        }
        enemy.ChangeState(new EnemyDashState(enemy, direction, state));
    }
}
