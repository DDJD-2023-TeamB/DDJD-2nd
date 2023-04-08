using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashable : MonoBehaviour
{
    Rigidbody _rigidbody;
    int _playerLayer;
    int _enemyLayer;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerLayer = LayerMask.NameToLayer("Player");
        _enemyLayer = LayerMask.NameToLayer("Enemy");
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
