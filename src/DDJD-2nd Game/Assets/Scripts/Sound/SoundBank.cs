using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBank : MonoBehaviour
{
    public static SoundBank Instance;

    [SerializeField]
    private FMODUnity.EventReference _pickupSound;

    [SerializeField]
    private FMODUnity.EventReference _buySound;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }
    }

    public FMODUnity.EventReference PickupSound
    {
        get { return _pickupSound; }
    }

    public FMODUnity.EventReference BuySound
    {
        get { return _buySound; }
    }
}
