using UnityEngine;

public class EnemyLineOfSight : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The angle of the line of sight in degrees")]
    [Range(0, 180)]
    private float _angle;

    [SerializeField]
    private float _distance = 20.0f;

    private BasicEnemy _enemy;

    public void Awake()
    {
        _enemy = GetComponent<BasicEnemy>();
    }

    public bool CanSeePlayer()
    {
        Vector3 position = transform.position + Vector3.up * 0.9f;
        Vector3 direction = _enemy.Player.transform.position - transform.position;
        direction.y = 0;
        float angle = Vector3.Angle(direction, transform.forward);
        float halfAngle = _angle * 0.5f;
        if (angle < halfAngle && angle > -halfAngle)
        {
            RaycastHit hit;
            if (
                Physics.Raycast(
                    position,
                    direction.normalized,
                    out hit,
                    _distance,
                    RayCastUtils.RayCastMask
                )
            )
            {
                if (hit.collider.gameObject == _enemy.Player.gameObject)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
