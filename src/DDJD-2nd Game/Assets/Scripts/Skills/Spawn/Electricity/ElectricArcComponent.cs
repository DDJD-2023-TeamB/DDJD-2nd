using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricArcComponent : SkillComponent
{
    private BoxCollider _collider;

    protected override void Awake() {
        base.Awake();
        _collider = GetComponent<BoxCollider>();

        Transform arcPos1 = transform.Find("Pos1");
        Transform arcPos4 = transform.Find("Pos4");
        float distance = Vector3.Distance(arcPos1.position, arcPos4.position);

        // _collider.size = new Vector3(distance, _collider.size.y, _collider.size.z);
        // Debug.Log("before" + _collider.center);
        // _collider.center = transform.position;
        // Debug.Log("after" + _collider.center);
    }

    protected override void OnImpact(Collider other, float multiplier = 1)
    {
        base.OnImpact(other, multiplier);
        Damage(
            other.gameObject,
            (int)(_skillStats.Damage * multiplier),
            other.ClosestPoint(transform.position),
            other.transform.forward
        );
    }
}
