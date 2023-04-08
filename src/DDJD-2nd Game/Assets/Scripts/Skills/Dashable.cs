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

    public void DashWithSkill(Dash dashSkill)
    {
        Quaternion rotation = Quaternion.LookRotation(transform.forward);
        GameObject spell = Instantiate(dashSkill.SpellPrefab, transform.position, rotation);
        DashComponent dashComponent = spell.GetComponent<DashComponent>();
        dashComponent.SetCaster(gameObject);
        dashComponent.SetSkill(dashSkill);

        // TODO: Improve movement after dash logic is implemented
        Vector3 force = transform.forward * dashSkill.DashStats.Force;
        _rigidbody.AddForce(force, ForceMode.Impulse);
    }
}
