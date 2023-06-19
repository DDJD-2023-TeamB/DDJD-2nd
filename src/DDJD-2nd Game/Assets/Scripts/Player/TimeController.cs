using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    private List<Rigidbody> _rigidbodies = new List<Rigidbody>();

    // Start is called before the first frame update
    void Awake() { }

    // Update is called once per frame
    void Update() { }

    public void Slowdown()
    {
        Time.timeScale = 0.2f;
        //Get all rigidbodies in radius 50
        Collider[] colliders = Physics.OverlapSphere(transform.position, 50.0f);
        foreach (Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                _rigidbodies.Add(rb);
            }
        }

        foreach (Rigidbody rb in _rigidbodies)
        {
            rb.interpolation = RigidbodyInterpolation.Interpolate;
        }
    }

    public void Reset()
    {
        Time.timeScale = 1f;
        foreach (Rigidbody rb in _rigidbodies)
        {
            if (rb == null)
            {
                continue;
            }
            rb.interpolation = RigidbodyInterpolation.None;
        }
        _rigidbodies.Clear();
    }
}
