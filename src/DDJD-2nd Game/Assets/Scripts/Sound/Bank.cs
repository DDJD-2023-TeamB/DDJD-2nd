using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    public static Bank Instance;

    [SerializeField]
    private SerializedDictionary<string, GameObject> _banks;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }
    }

    public GameObject Get(string key)
    {
        if (_banks.ContainsKey(key))
        {
            return _banks[key];
        }
        Debug.LogError("Key not found in SoundBank: " + key);
        return null;
    }
}
