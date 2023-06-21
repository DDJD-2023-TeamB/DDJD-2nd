using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace BrokenVector.PersistentComponents
{
    public partial class PersistentComponents
    {
        private PersistencyData persistencyData;

        private void RememberComponents()
        {
            if (Settings.ComponentsStayPersistent)
            {
                List<int> saveData = new List<int>();
                foreach (var pair in components)
                    foreach (var instanceID in pair.Value)
                        saveData.Add(instanceID);

                persistencyData.persistentComponents = saveData.ToArray();
                persistencyData.Save();
            }
        }
        private void RecallComponents()
        {
#if UNITY_5_4_OR_NEWER
            persistencyData = AssetDatabase.LoadAssetAtPath<PersistencyData>(PersistencyData.GetAssetLocation());
#else
            persistencyData = (PersistencyData) AssetDatabase.LoadAssetAtPath(PersistencyData.GetAssetLocation(), typeof(PersistencyData));
#endif
            if (persistencyData == null)
                persistencyData = PersistencyData.CreateInstance(new int[0]);

            if (Settings.ComponentsStayPersistent)
            {
                foreach (var instanceID in persistencyData.persistentComponents)
                {
                    var obj = (Component)EditorUtility.InstanceIDToObject(instanceID);
                    if (obj == null)
                        continue;
                    if (!components.ContainsKey(obj.gameObject))
                        components[obj.gameObject] = new List<int>();

                    components[obj.gameObject].Add(instanceID);
                }
            }
        }
    }
}