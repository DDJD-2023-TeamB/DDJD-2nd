using UnityEngine;

public class EnemyAimComponent : MonoBehaviour, AimComponent
{
    private RangedEnemy _enemy;
    private Vector3 _aimDirection;

    private GameObject _leftRune;
    private GameObject _rightRune;

    private void Start()
    {
        _enemy = GetComponent<RangedEnemy>();
    }

    public void StartAim()
    {
        if (_leftRune == null)
        {
            if (_enemy.EnemySkills.LeftSkill != null)
                _leftRune = _enemy.EnemySkills.LeftSkill.ActivateRune(_enemy.LeftSpellOrigin);
        }
        if (_rightRune == null)
        {
            if (_enemy.EnemySkills.RightSkill != null)
                _rightRune = _enemy.EnemySkills.LeftSkill.ActivateRune(_enemy.RightSpellOrigin);
        }
    }

    public void StopAim()
    {
        GameObject[] runes = new GameObject[] { _leftRune, _rightRune };
        foreach (GameObject rune in runes)
        {
            if (rune != null)
            {
                Destroy(rune);
            }
        }
    }

    public Quaternion GetAimRotation()
    {
        _aimDirection = _enemy.Player.transform.position - transform.position;
        _aimDirection.y = 0;
        return Quaternion.LookRotation(_aimDirection);
    }

    public Vector3 GetAimDirection(Vector3 origin, bool rayCast = true)
    {
        _aimDirection = _enemy.Player.transform.position - origin;

        return _aimDirection;
    }

    public bool CanHitPlayer()
    {
        Vector3 position = transform.position + Vector3.up * 0.8f;
        Vector3 direction = GetAimDirection(position);
        return CanHitPlayer(position, direction);
    }

    public bool CanHitPlayer(Vector3 origin, Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, _enemy.AttackRange))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                return true;
            }

            //If collider is farther than player, can probably hit player
            if (hit.distance > Vector3.Distance(origin, _enemy.Player.transform.position))
            {
                return true;
            }
        }

        return false;
    }

    public bool GetAimRaycastHit(out RaycastHit hit)
    {
        // TODO Check if this is correct
        return Physics.Raycast(
            _enemy.Player.transform.position,
            _enemy.Player.transform.forward,
            out hit,
            RayCastUtils.RayCastMask
        );
    }
}
