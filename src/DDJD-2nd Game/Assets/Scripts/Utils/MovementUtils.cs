using UnityEngine;

public class MovementUtils
{
    public static bool IsGrounded(Rigidbody rigidbody)
    {
        Vector3 position = rigidbody.position;
        position.y += 0.1f;
        bool found = Physics.Raycast(position, Vector3.down, 0.5f, RayCastUtils.PhysicalMask);
        return found;
    }
}
