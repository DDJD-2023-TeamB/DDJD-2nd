using UnityEngine;

public class NoiseSource : MonoBehaviour
{
    public void MakeNoise(float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider collider in colliders)
        {
            NoiseListener listener = collider.GetComponent<NoiseListener>();
            if (listener != null)
            {
                listener.HearNoise(transform.position);
            }
        }
    }
}
