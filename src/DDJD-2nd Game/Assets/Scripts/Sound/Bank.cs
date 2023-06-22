using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    public static Bank Instance;

    [SerializeField]
    private GameObject _canvasIcon;

    [SerializeField]
    private GameObject _canvasText;

    [SerializeField]
    private Material _outline;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }
    }

    public GameObject CanvasIcon
    {
        get { return _canvasIcon; }
    }

    public GameObject CanvasText
    {
        get { return _canvasText; }
    }

    public Material Outline
    {
        get { return _outline; }
    }
}
