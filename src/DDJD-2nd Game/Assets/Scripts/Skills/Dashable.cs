using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashable : MonoBehaviour
{
    Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void DashWithSkill(DashSkill dashSkill)
    {
        Quaternion rotation = Quaternion.LookRotation(transform.forward);
        GameObject spell = Instantiate(dashSkill.SpellPrefab, transform.position, rotation);
        DashComponent dashComponent = spell.GetComponent<DashComponent>();
        dashComponent.SetCaster(gameObject);
        dashComponent.SetSkill(dashSkill);

        Dash(dashSkill.DashStats);
    }

    public void Dash(DashStats stats)
    {
        // TODO: Improve movement after dash logic is implemented
        Vector3 force = transform.forward * stats.Force;
        _rigidbody.AddForce(force, ForceMode.Impulse);
    }
}
