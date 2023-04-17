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
        foreach (Rigidbody rb in rigidbodies)
        {
            float distance = Vector3.Distance(rb.transform.position, hitPoint);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestRb = rb;
            }
        }

        //Add force to closest rigidbody
        closestRb.AddForce(hitDirection * force * _knockbackForceMultiplier, ForceMode.Impulse);

        //Draw debug lines
        Debug.DrawLine(hitPoint, hitPoint + hitDirection * force, Color.red, 20f);

        //Draw debug sphere on hitpoint
        Debug.DrawLine(hitPoint, hitPoint + new Vector3(0, 1, 0), Color.blue, 20f);
    }
}
