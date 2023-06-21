using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace BrokenVector.PersistentComponents
{
    public partial class PersistentComponents
    {
        public void WatchComponent(Component comp)
        {
            if (IsComponentWatched(comp))
                return;

            if (!components.ContainsKey(comp.gameObject))
                components[comp.gameObject] = new List<int>();
            components[comp.gameObject].Add(comp.GetInstanceID());

            UpdateComponent(comp);

            if (PersistentComponentsWindow.Instance != null)
                PersistentComponentsWindow.Instance.Repaint();

            EditorApplication.RepaintHierarchyWindow();
        }

        public void ForgetComponent(Component comp)
        {
            components[comp.gameObject].Remove(comp.GetInstanceID());
            if (components[comp.gameObject].Count == 0)
                components.Remove(comp.gameObject);

            serializedObjects.Remove(comp.GetInstanceID());

            if (PersistentComponentsWindow.Instance != null)
                PersistentComponentsWindow.Instance.Repaint();

            EditorApplication.RepaintHierarchyWindow();
        }

        public void WatchComponents(params Component[] comps)
        {
            foreach (var c in comps)
                WatchComponent(c);
        }
        public void ForgetComponents(params Component[] comps)
        {
            foreach (var c in comps)
                ForgetComponent(c);
        }
        public void ForgetEveryComponent()
        {
            List<Component> toForget = new List<Component>();
            foreach (var pair in components)
                foreach (var comp in pair.Value)
                    toForget.Add((Component)EditorUtility.InstanceIDToObject(comp));

            ForgetComponents(toForget.ToArray());
        }

        public bool IsComponentWatched(Component comp)
        {
            if (comp == null)
                return false;
            return (components.ContainsKey(comp.gameObject) && components[comp.gameObject].Contains(comp.GetInstanceID()));
        }
    }
}