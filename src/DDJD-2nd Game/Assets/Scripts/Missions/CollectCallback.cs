using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CollectCallback : MonoBehaviour
{
    private static string path = "Assets/Prefabs/Missions/Goals/Collect/";

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void OnCollectGoalStarted(CollectibleGoalData data)
    {
        foreach (GameObject prefab in data.Prefabs)
        {
            // Load the prefab at the given path
            if (prefab != null)
            {
                Instantiate(prefab);
            }
        }
    }

    public void Print()
    {
        Debug.Log("PRINTING..");
    }
}
