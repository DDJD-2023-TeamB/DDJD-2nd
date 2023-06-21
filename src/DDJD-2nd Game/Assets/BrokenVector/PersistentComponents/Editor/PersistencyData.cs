using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace BrokenVector.PersistentComponents
{
    public class PersistencyData : ScriptableObject
    {
        public int[] persistentComponents;

        public static PersistencyData CreateInstance(int[] c)
        {
            var instance = ScriptableObject.CreateInstance<PersistencyData>();
            instance.persistentComponents = c;

            AssetDatabase.CreateAsset(instance, GetAssetLocation());
            AssetDatabase.SaveAssets();

            return instance;
        }

        public void Save()
        {
            AssetDatabase.SaveAssets();
        }

        public static string GetAssetLocation()
        {
            var me = AssetDatabase.FindAssets("PersistencyData")[0];
            var myPath = AssetDatabase.GUIDToAssetPath(me);
            myPath = myPath.Remove(myPath.Length - 19);
            return myPath + "/" + Constants.ASSET_SAVEDATA_NAME;
        }

    }
}
