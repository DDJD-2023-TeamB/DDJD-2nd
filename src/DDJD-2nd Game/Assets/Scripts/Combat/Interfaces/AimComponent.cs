using UnityEngine;

public interface AimComponent
{
    void StartAim();
    void StopAim();
    bool GetAimRaycastHit(out RaycastHit hit);
    Vector3 GetAimDirection(Vector3 origin, bool rayCast = true);
    Quaternion GetAimRotation();
}
