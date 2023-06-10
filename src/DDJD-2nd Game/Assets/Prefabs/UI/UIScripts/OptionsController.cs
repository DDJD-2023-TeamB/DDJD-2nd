using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    [SerializeField] private GameObject uiController;
    private CameraController cameraController;

    [SerializeField] private GameObject mouseSensitivityScrollBarObject;

    private void Start()
    {
        cameraController = uiController.GetComponent<PlayerUI>().playerObject.GetComponent<CameraController>();
        mouseSensitivityScrollBarObject.GetComponent<Scrollbar>().onValueChanged.AddListener(setCameraSensitivity);
    }
    public void setGameVolume(float value)
    {
        Debug.Log("Volume change not yet implemented on OptionsController.cs!");
    }

    public void setCameraSensitivity(float value)
    {
        cameraController.setCameraRotationSpeed(value*30+5); //Go from 5 to 35
    }
}
