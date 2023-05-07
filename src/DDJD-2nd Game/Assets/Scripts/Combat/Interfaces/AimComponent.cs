using UnityEngine;

public interface AimComponent
{
    void StartAim();
    void StopAim();

    Vector3 GetAimDirection(Vector3 origin, bool rayCast = true);
    Quaternion GetAimRotation();
}
