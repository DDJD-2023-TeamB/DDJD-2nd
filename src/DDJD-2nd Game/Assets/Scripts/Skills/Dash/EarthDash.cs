using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthDash : DashComponent
{
    protected override void Awake()
    {
        base.Awake();
        Vector3 position = transform.position;
        position.y = 0.0f;
        transform.position = position;
        Destroy(gameObject, 5.0f);
    }
}
