using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    [SerializeField]
    private UIController _uiController;
    private CameraController cameraController;

    [SerializeField]
    private GameObject mouseSensitivityScrollBarObject;

    private SoundEmitter _soundEmitter;
    private FMOD.Studio.PARAMETER_ID _menuParameterId;

    public void SetUIController(UIController uiController)
    {
        _uiController = uiController;
        cameraController = _uiController.Player.CameraController;
        mouseSensitivityScrollBarObject
            .GetComponent<Scrollbar>()
            .onValueChanged.AddListener(SetCameraSensitivity);
    }

    private void Awake()
    {
        _soundEmitter = GetComponent<SoundEmitter>();
    }

    private void Start()
    {
        _menuParameterId = _soundEmitter.GetParameterId("menu", "Main Menu Type");
    }

    public void setGameVolume(float value)
    {
        Debug.LogError("Volume change not yet implemented on OptionsController.cs!");
    }

    public void SetCameraSensitivity(float value)
    {
        cameraController.setCameraRotationSpeed(value * 30 + 5); //Go from 5 to 35
    }
}
