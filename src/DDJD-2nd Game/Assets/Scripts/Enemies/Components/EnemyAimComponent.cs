using UnityEngine;

public class EnemyAimComponent : MonoBehaviour, AimComponent
{
    private RangedEnemy _enemy;
    private Transform _enemyTransform;
    private Vector3 _aimDirection;

    private GameObject _leftRune;
    private GameObject _rightRune;

    private void Start()
    {
        _enemy = GetComponent<RangedEnemy>();
        _enemyTransform = _enemy.transform;
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
        _aimDirection = _enemy.Player.transform.position - _enemyTransform.position;
        _aimDirection.y = 0;
        return Quaternion.LookRotation(_aimDirection);
    }

    public Vector3 GetAimDirection(Vector3 origin, bool rayCast = true)
    {
        _aimDirection = _enemy.Player.transform.position - origin;

        return _aimDirection;
    }
}
