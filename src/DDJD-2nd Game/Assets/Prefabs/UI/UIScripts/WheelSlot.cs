using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WheelSlot : MonoBehaviour
{
    Camera cam;
    EventSystem eventSys;
    public GameObject MouseHelper;
    public Boolean mouseOver = false;

    private void Start()
    {
        cam = Camera.main;
        eventSys = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }

    private void Update() { }
}
