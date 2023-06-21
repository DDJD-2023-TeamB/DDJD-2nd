using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

namespace BrokenVector.PersistentComponents
{
    [CanEditMultipleObjects]
#if UNITY_5_4_OR_NEWER
    [CustomEditor(typeof(Component), true, isFallback = true)]
# else
    [CustomEditor(typeof(Component), true)]
#endif
    public class CustomInspector : UnityEditor.Editor
    {
        private static GUIStyle toolbarButtonStyle;

        public override void OnInspectorGUI()
        {
            if (!Settings.ShowCustomInspector || (!Application.isPlaying && !Settings.ComponentsStayPersistent))
            {
                base.OnInspectorGUI();
                return;
            }

            Component[] components = new Component[targets.Length];
            for (int i = 0; i < targets.Length; i++)
                components[i] = targets[i] as Component;

            if (toolbarButtonStyle == null)
                toolbarButtonStyle = (GUIStyle)"PreButton";

            Color bgColorBefore = GUI.backgroundColor;

            bool allWatched = true;
            foreach(var comp in components)
                if (!PersistentComponents.Instance.IsComponentWatched(comp))
                {
                    allWatched = false;
                    break;
                }
            if (allWatched)
                GUI.backgroundColor = Color.green;

            GUILayout.Space(10);

            using (new GUILayout.HorizontalScope())
            {
                if (allWatched)
                {
                    if (GUILayout.Button("Persistent", toolbarButtonStyle))
                        PersistentComponents.Instance.ForgetComponents(components);
                }
                else
                {
                    if (GUILayout.Button("Not Persistent", toolbarButtonStyle))
                        PersistentComponents.Instance.WatchComponents(components);
                }
            }

            GUILayout.Space(10);

            DrawDefaultInspector();

            GUI.backgroundColor = bgColorBefore;
        }
    }
}