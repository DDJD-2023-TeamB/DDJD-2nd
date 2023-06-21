using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace BrokenVector.PersistentComponents
{

    public partial class PersistentComponents
    {
        private static PersistentComponents instance;
        public static PersistentComponents Instance
        {
            get
            {
                if (instance == null)
                    instance = new PersistentComponents();
                return instance;
            }
        }

        public Dictionary<GameObject, List<int>> WatchedComponents { get { return components; } }

        private Dictionary<GameObject, List<int>> components = new Dictionary<GameObject, List<int>>();
        private Dictionary<int, SerializedObject> serializedObjects = new Dictionary<int, SerializedObject>();

        public PersistentComponents()
        {
            EditorApplication.playmodeStateChanged += OnPlaymodeChanged;
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyItemCallback;

            RecallComponents();
        }

        public void OnPlaymodeChanged()
        {
            if (EditorApplication.isPlaying)
            {
                UpdateAllComponents();
            }
            if (!EditorApplication.isPlaying && !EditorApplication.isPlayingOrWillChangePlaymode)
            {
                OnExitPlayMode();   
            }

            RememberComponents();
        }

        private void OnExitPlayMode()
        {
            Instance.ApplyModifiedProperties();
        }

        public void ApplyModifiedProperties()
        {
            List<int> toRemove = new List<int>();
            foreach (var pair in serializedObjects)
            {
                if (pair.Value.targetObject != null)
                    pair.Value.ApplyModifiedProperties();
                else
                    toRemove.Add(pair.Key);
            }
            foreach(var k in toRemove)
            {
                serializedObjects.Remove(k);
            }
        }

        public void UpdateComponent(Component comp)
        {
            if (!IsComponentWatched(comp))
                return;

            var instanceId = comp.GetInstanceID();
            if (!serializedObjects.ContainsKey(instanceId))
            {
                serializedObjects.Add(instanceId, new SerializedObject(comp));
            }

            var clone = new SerializedObject(comp);
            var original = serializedObjects[instanceId];

            SerializedProperty sp = clone.GetIterator();
            while (sp.NextVisible(true))
            {
                original.CopyFromSerializedProperty(sp);
            }
            sp.Reset();
        }
        public void UpdateComponents(params Component[] comps)
        {
            foreach (var c in comps)
                UpdateComponent(c);
        }
        public void UpdateAllComponents()
        {
            foreach (var go in components)
                foreach(var component in go.Value)
                {
                    UpdateComponent((Component)EditorUtility.InstanceIDToObject(component));
                }
        }

        private static void HierarchyItemCallback(int instanceID, Rect selectionRect)
        {
            bool persistent = false;
            Transform objTransform = null;
            foreach(var pair in Instance.components)
                if(pair.Key.GetInstanceID() == instanceID)
                {
                    persistent = true;
                    objTransform = pair.Key.transform;
                    break;
                }
            if (!persistent)
                return;

            int numParents = 0;
            while(objTransform.parent != null)
            {
                numParents++;
                objTransform = objTransform.parent;
            }

            Rect r = new Rect(selectionRect);
            r.x = selectionRect.x - numParents * 14 - 25;
            r.width = 18;

            GUI.Label(r, "P");
        }

    }

}
