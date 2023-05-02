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

    public override void SetSkill(Skill skill)
    {
        base.SetSkill(skill);
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void SetCaster(GameObject caster)
    {
        base.SetCaster(caster);
        //transform.parent = caster.transform;
        //transform.parent = caster.transform.parent;
    }

    public override void SetDashDirection(Vector3 direction)
    {
        base.SetDashDirection(direction);
        Vector3 groundDirection = new Vector3(direction.x, 0.0f, direction.z);
        //transform.position -= groundDirection * 0.75f;
    }
}
