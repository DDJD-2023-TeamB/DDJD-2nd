using UnityEngine;
using System.Collections;
using System;

public class EnemyDashable : Dashable
{
    private Vector3 _dashDirection = Vector3.zero;
    private bool _dashReady = true;

    public Action OnDashFinish;

    public void Dash(Vector3 direction, DashStats stats)
    {
        _dashDirection = direction;
        base.Dash(stats);
        StartCoroutine(DashReadyCoroutine(stats));
    }

    public void DashWithSkill(Vector3 direction, DashSkill skill)
    {
        _dashDirection = direction;
        base.DashWithSkill(skill);
        StartCoroutine(DashReadyCoroutine(skill.DashStats));
    }

    protected override Vector3 GetDashDirection()
    {
        return _dashDirection;
    }

    protected override void SetDashAnimation() { }

    private IEnumerator DashReadyCoroutine(DashStats stats)
    {
        _dashReady = false;
        yield return new WaitForSeconds(stats.Duration + 0.5f);
        OnDashFinish?.Invoke();
        yield return new WaitForSeconds(3.0f);
        _dashReady = true;
    }

    public bool IsDashReady()
    {
        return _dashReady;
    }
}
