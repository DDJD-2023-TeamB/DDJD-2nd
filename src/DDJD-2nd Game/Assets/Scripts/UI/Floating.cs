using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Floating : MonoBehaviour
{
    private Transform _mainCam;

    // Start is called before the first frame update
    void Start()
    {
        _mainCam = Camera.main.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(_mainCam.position);
    }
}
