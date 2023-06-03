using UnityEngine;

public static class RayCastUtils
{
    public static int RayCastMask = ~(
        LayerMask.GetMask("PlayerTrigger") | LayerMask.GetMask("Ignore Raycast")
    );
}
