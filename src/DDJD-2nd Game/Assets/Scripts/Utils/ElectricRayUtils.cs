using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricRayUtils
{
    public static void SetBezierAndScale(Transform transform, Vector3 origin, Vector3 end)
    {
        // Calculate Bezier arc
        Vector3 pos2 = 2 * origin / 3 + end / 3;
        Vector3 pos3 = origin / 3 + 2 * end / 3;

        // Get bezier points
        Transform arcPos1 = transform.Find("Pos1");
        Transform arcPos2 = transform.Find("Pos2");
        Transform arcPos3 = transform.Find("Pos3");
        Transform arcPos4 = transform.Find("Pos4");

        // Scale object for correct box collider
        float realDistance = Vector3.Distance(origin, end);
        Debug.Log("origin" + origin);
        Debug.Log("end" + end);
        Debug.Log("realDistance" + realDistance);
        float previousDistance = Vector3.Distance(arcPos1.position, arcPos4.position);
        Debug.Log("previousDistance" + previousDistance);
        float scale = realDistance / previousDistance;
        transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);

        // Set VFX positions
        arcPos1.position = origin;
        arcPos2.position = pos2;
        arcPos3.position = pos3;
        arcPos4.position = end;
    }
}
