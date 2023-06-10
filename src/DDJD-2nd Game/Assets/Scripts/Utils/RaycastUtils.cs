using UnityEngine;

public static class RayCastUtils
{
    public static int RayCastMask = ~(
        LayerMask.GetMask("PlayerTrigger")
        | LayerMask.GetMask("Ignore Raycast")
        | LayerMask.GetMask("Attack")
        | LayerMask.GetMask("CollideEnvironment")
        | LayerMask.GetMask("ElementSource")
    );

    public static int PhysicalMask = ~(
        LayerMask.GetMask("PlayerTrigger")
        | LayerMask.GetMask("Ignore Raycast")
        | LayerMask.GetMask("Attack")
        | LayerMask.GetMask("ElementSource")
    );
}
