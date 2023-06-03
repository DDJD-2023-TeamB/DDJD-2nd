using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelHiglighterContoller : MonoBehaviour
{
    // Start is called before the first frame update

    float targetAngle;
    public float rotationSpeed = 800f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float currAngle = transform.rotation.eulerAngles.z;
        if (currAngle > 180)
        {
            currAngle -= 360;
        }
        float remaniningAngle = targetAngle - currAngle;
        if (remaniningAngle>180)
        {
            remaniningAngle -= 360;
        }
        if (remaniningAngle < -180)
        {
            remaniningAngle += 360;
        }
        //Debug.Log("Cur Angle: " + currAngle + "    Tar Angle: " + targetAngle + "     Rem Angle: " + remaniningAngle + "     Rotation Speed: " + rotationSpeed);
        if(Mathf.Abs(remaniningAngle) < rotationSpeed * Time.deltaTime)
        {
            transform.rotation = Quaternion.Euler(0,0,targetAngle);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0,0,transform.rotation.eulerAngles.z + Mathf.Sign(remaniningAngle)*rotationSpeed*Time.deltaTime);
            //Debug.Log("Cur Angle: " + transform.rotation.z + "    Tar Angle: " + targetAngle + "     Rotating...");
        }
        
    }

    public void setTargetAngle(float angle)
    {
        //Debug.Log("Changing higlhihting angle to " + angle);
        this.targetAngle = angle;
    }
}
