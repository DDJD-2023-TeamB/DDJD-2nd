using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidEnemy : Enemy
{
    private Animator _animator;
    private RagdollController _ragdollController;

    override public void Awake()
    {
        base.Awake();
        _animator = GetComponent<Animator>();
        _ragdollController = GetComponent<RagdollController>();
        _ragdollController.DeactivateRagdoll();
    }

    // Update is called once per frame
    public override void  Update()
    {
        base.Update();
    }

    public override void TakeDamage(int damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        base.TakeDamage(damage, hitPoint, hitDirection);
        _animator.SetTrigger("Hit");
    }

    public override void Die(int force, Vector3 hitPoint, Vector3 hitDirection)
    {
        _ragdollController.ActivateRagdoll();
        _ragdollController.PushRagdoll(force, hitPoint, hitDirection);

        Destroy(gameObject, 5f);
    }
}
