using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidEnemy : Enemy
{
    private Animator _animator;
    private RagdollController _ragdollController;

    private DisappearEffect _disappearEffect;

    override public void Awake()
    {
        base.Awake();
        _animator = GetComponent<Animator>();
        _ragdollController = GetComponent<RagdollController>();
        _disappearEffect = GetComponent<DisappearEffect>();
    }

    override public void Start()
    {
        base.Start();
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
        
        StartCoroutine(WaitAndDie(3f));
    }

    protected IEnumerator  WaitAndDie(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Transform ragdollTransform = _ragdollController.GetRagdollTransform();
        SpawnDeathVFX(ragdollTransform.position);
    }
}