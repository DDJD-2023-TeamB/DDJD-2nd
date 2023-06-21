using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class CollectCallback : MonoBehaviour
{
    private static string path = "Assets/Prefabs/Missions/Goals/Collect/";
    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void OnCollectGoalStarted(string collectGoalName)
    {
        string[] prefabPaths = AssetDatabase.FindAssets("t:Prefab", new[] { path + collectGoalName});

        foreach (string prefabPath in prefabPaths)
        {
            // Load the prefab at the given path
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(prefabPath));

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
