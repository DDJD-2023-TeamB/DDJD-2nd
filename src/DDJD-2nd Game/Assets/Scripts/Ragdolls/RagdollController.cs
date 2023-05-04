using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    private Rigidbody[] _rigidbodies; //Rig rigibodies
    private Collider[] _colliders; //Rig colliders
    private Collider _collider; //Main collider
    private Rigidbody _rb; //Main rigidbody
    private Animator _animator;

    [SerializeField]
    private float _knockbackForceMultiplier = 3f;

    private BoneTransform[] _standUpBoneTransforms;
    public BoneTransform[] StandUpBoneTransforms
    {
        get { return _standUpBoneTransforms; }
    }

    private Transform[] _bones;

    public Transform[] Bones
    {
        get { return _bones; }
    }

    private Transform _hipsBone;

    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
        //Remove main rigidbody from array
        List<Rigidbody> rigidbodies = new List<Rigidbody>(_rigidbodies);
        rigidbodies.Remove(_rb);
        _rigidbodies = rigidbodies.ToArray();

        _colliders = GetComponentsInChildren<Collider>();
        _collider = GetComponent<Collider>();
        _animator = GetComponent<Animator>();
        _hipsBone = _animator.GetBoneTransform(HumanBodyBones.Hips);
        _bones = GetRagdollTransforms();
        _standUpBoneTransforms = new BoneTransform[_bones.Length];

        PopulateStandUpBones("StandUpFaceDown");
    }

    public Transform GetRagdollTransform()
    {
        return _hipsBone.transform;
    }

    public void ActivateRagdoll()
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = false;
        }
        foreach (Collider col in _colliders)
        {
            col.enabled = true;
        }
        _collider.enabled = false;
        _rb.isKinematic = true;
        _animator.enabled = false;
    }

    public float GetVelocity()
    {
        return _rigidbodies[0].velocity.magnitude;
    }

    public Transform[] GetRagdollTransforms()
    {
        return _hipsBone.GetComponentsInChildren<Transform>();
    }

    public void DeactivateRagdoll()
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = true;
        }
        foreach (Collider col in _colliders)
        {
            col.enabled = false;
        }
        _collider.enabled = true;
        _rb.isKinematic = false;
        _animator.enabled = true;
    }

    public void PushRagdoll(int force, Vector3 hitPoint, Vector3 hitDirection)
    {
        //Vector3 direction = hitPoint - transform.position;
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        //Get rigidbody closest to hitpoint
        Rigidbody closestRb = rigidbodies[0];
        float closestDistance = Vector3.Distance(rigidbodies[0].transform.position, hitPoint);
        float totalForce = force * _knockbackForceMultiplier;
        foreach (Rigidbody rb in rigidbodies)
        {
            float distance = Vector3.Distance(rb.transform.position, hitPoint);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestRb = rb;
            }
            rb.AddForce(totalForce * 0.2f * hitDirection, ForceMode.Impulse);
        }

        //Add force to closest rigidbody
        closestRb.AddForce(hitDirection * totalForce, ForceMode.Impulse);
    }

    public void AlignPositionWithHips()
    {
        Vector3 originalPosition = _hipsBone.position;
        transform.position = _hipsBone.position;

        Vector3 positionOffset = _standUpBoneTransforms[0].Position;
        positionOffset.y = 0;
        positionOffset = transform.rotation * positionOffset;
        transform.position -= positionOffset;

        if (Physics.Raycast(_hipsBone.position, Vector3.down, out RaycastHit hit, 1f))
        {
            transform.position = new Vector3(
                transform.position.x,
                hit.point.y,
                transform.position.z
            );
        }

        _hipsBone.position = originalPosition;
    }

    public void AlignRotationWithHips()
    {
        Vector3 originalPosition = _hipsBone.position;
        Quaternion originalHipsRotaiton = _hipsBone.rotation;

        Vector3 desiredDirection = _hipsBone.up * 1f;
        desiredDirection.y = 0;
        desiredDirection.Normalize();

        Quaternion fromToRotation = Quaternion.FromToRotation(transform.forward, desiredDirection);
        transform.rotation *= fromToRotation;

        _hipsBone.position = originalPosition;
        _hipsBone.rotation = originalHipsRotaiton;
    }

    public void PopulateStandUpBones(string animationName)
    {
        Vector3 positionBeforeSampling = transform.position;
        Quaternion rotationBeforeSampling = transform.rotation;
        foreach (AnimationClip clip in _animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == animationName)
            {
                clip.SampleAnimation(gameObject, 0f);
                break;
            }
        }
        PopulateBoneTransforms(_standUpBoneTransforms);

        transform.position = positionBeforeSampling;
        transform.rotation = rotationBeforeSampling;
    }

    private void PopulateBoneTransforms(BoneTransform[] boneTransforms)
    {
        for (int i = 0; i < boneTransforms.Length; i++)
        {
            Transform bone = _bones[i].transform;
            boneTransforms[i] = new BoneTransform(bone.localPosition, bone.localRotation);
        }
    }
}
