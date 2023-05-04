using UnityEngine;
using System.Collections.Generic;

public class EnemyRecoveringResetBonesState : GenericState
{
    protected BasicEnemy _context;
    private string _recoveringAnimationName;
    private BoneTransform[] _standUpBoneTransforms;
    private BoneTransform[] _ragdollBoneTransforms;

    private float _elapsedResetBonesTime = 0.0f;

    public EnemyRecoveringResetBonesState(BasicEnemy enemy)
        : base(enemy)
    {
        _context = enemy;

        bool isfacingUp = _context.RagdollController.HipsBone.forward.y > 0;
        _recoveringAnimationName = isfacingUp ? "StandUpFaceUp" : "StandUpFaceDown";
        _standUpBoneTransforms = _context.RagdollController.GetAnimationInitialBones(
            _recoveringAnimationName
        );
        _ragdollBoneTransforms = new BoneTransform[_context.RagdollController.Bones.Length];
    }

    public override void Enter()
    {
        _context.RagdollController.AlignRotationWithHips();
        _context.RagdollController.AlignPositionWithHips(_recoveringAnimationName);
        PopulateBoneTransforms(_ragdollBoneTransforms);
    }

    public override void StateUpdate()
    {
        _elapsedResetBonesTime += Time.deltaTime;
        float percentage = _elapsedResetBonesTime / 0.5f;

        for (int i = 0; i < _context.RagdollController.Bones.Length; i++)
        {
            Transform bone = _context.RagdollController.Bones[i];
            bone.localPosition = Vector3.Lerp(
                _ragdollBoneTransforms[i].Position,
                _standUpBoneTransforms[i].Position,
                percentage
            );
            bone.localRotation = Quaternion.Lerp(
                _ragdollBoneTransforms[i].Rotation,
                _standUpBoneTransforms[i].Rotation,
                percentage
            );
        }

        if (percentage >= 1.0f)
        {
            _context.ChangeState(new EnemyRecoveringState(_context, _recoveringAnimationName));
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override bool CanChangeState(GenericState state)
    {
        return true;
    }

    private void PopulateStandUpBones(string animationName)
    {
        Vector3 positionBeforeSampling = _context.transform.position;
        Quaternion rotationBeforeSampling = _context.transform.rotation;
        foreach (AnimationClip clip in _context.Animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == animationName)
            {
                clip.SampleAnimation(_context.gameObject, 0f);
                break;
            }
        }
        PopulateBoneTransforms(_standUpBoneTransforms);

        _context.transform.position = positionBeforeSampling;
        _context.transform.rotation = rotationBeforeSampling;
    }

    private void PopulateBoneTransforms(BoneTransform[] boneTransforms)
    {
        for (int i = 0; i < boneTransforms.Length; i++)
        {
            Transform bone = _context.RagdollController.Bones[i].transform;
            boneTransforms[i] = new BoneTransform(bone.localPosition, bone.localRotation);
        }
    }
}
