using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDashComponent : DashComponent
{
    [SerializeField]
    private float _explosionRadius = 5.0f;

    protected override void Start()
    {
        base.Start();
        transform.position -= new Vector3(0f, 0.75f, 0f);
    }

    public override void SetSkill(Skill skill)
    {
        base.SetSkill(skill);
        Destroy(gameObject, _skill.DashSkillStats.EffectDuration);
        Explode();
    }

    //explode on position
    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != _caster && collider.gameObject != gameObject)
            {
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(
                        _skillStats.ForceWithDamage(),
                        transform.position,
                        _explosionRadius,
                        0.0f,
                        ForceMode.Impulse
                    );
                }
                Damage(
                    collider.gameObject,
                    (int)(_skillStats.Damage),
                    (int)(_skillStats.ForceWithDamage()),
                    collider.ClosestPoint(_caster.transform.position),
                    _caster.transform.forward
                );
            }
        }
    }
}
