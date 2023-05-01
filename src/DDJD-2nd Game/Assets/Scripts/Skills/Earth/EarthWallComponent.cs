using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthWallComponent : StaticSkillComponent
{
    protected override void Awake()
    {
        base.Awake();
        _collider.enabled = false;
    }

    public override void Shoot(Vector3 direction)
    {
        base.Shoot(direction);

        // Draw a raycast from the player to the mouse position
        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, 100f);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject == gameObject)
            {
                continue;
            }
            transform.position = hit.point;
            transform.localRotation = Quaternion.LookRotation(
                _caster.transform.forward,
                hit.normal
            );
            _collider.enabled = true;
        }
    }
}
