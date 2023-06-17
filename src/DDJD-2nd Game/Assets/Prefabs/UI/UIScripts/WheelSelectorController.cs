using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSelectorController : MonoBehaviour
{
    float targetAngle;
    public float rotationSpeed = 1200f;

    private int _currentSlot = 0;

    private void Update()
    {
        float currAngle = transform.rotation.eulerAngles.z;
        float deltaTime = Time.deltaTime * 1 / Time.timeScale;
        if (currAngle > 180)
        {
            currAngle -= 360;
        }
        float remaniningAngle = targetAngle - currAngle;
        if (remaniningAngle > 180)
        {
            remaniningAngle -= 360;
        }
        if (remaniningAngle < -180)
        {
            remaniningAngle += 360;
        }
        if (Mathf.Abs(remaniningAngle) < rotationSpeed * deltaTime)
        {
            transform.rotation = Quaternion.Euler(0, 0, targetAngle);
        }
        else
        {
            transform.rotation = Quaternion.Euler(
                0,
                0,
                transform.rotation.eulerAngles.z
                    + Mathf.Sign(remaniningAngle) * rotationSpeed * deltaTime
            );
        }
    }

    public void setTargetAngle(float angle)
    {
        this.targetAngle = angle;
    }

    public void changeSelection(int slot)
    {
        _currentSlot = slot;
        switch (slot)
        {
            case 0:
                setTargetAngle(120);
                break;
            case 1:
                setTargetAngle(60);
                break;
            case 2:
                setTargetAngle(0);
                break;
            case 3:
                setTargetAngle(-60);
                break;
            case 4:
                setTargetAngle(-120);
                break;
            case 5:
                setTargetAngle(180);
                break;
        }
    }

    public int CurrentSlot
    {
        get { return _currentSlot; }
    }
}
