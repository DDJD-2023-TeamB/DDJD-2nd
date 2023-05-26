using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthDash : DashComponent
{
    protected override void Awake()
    {
        base.Awake();

        RaycastHit hit;
        Vector3 position = transform.position;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 100f))
        {
            position = hit.point;
        }
        else
        {
            position.y = 0.0f;
        }

        transform.position = position;
        Destroy(gameObject, 5.0f);
    }
}
