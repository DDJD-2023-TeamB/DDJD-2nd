using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CollectCallback : MonoBehaviour
{
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
}
