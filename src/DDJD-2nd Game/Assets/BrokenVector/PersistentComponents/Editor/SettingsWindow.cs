using System.Collections;
using System.Collections.Generic;
using BrokenVector.PersistentComponents.Utils;
using UnityEditor;
using UnityEngine;

namespace BrokenVector.PersistentComponents
{
	public class SettingsWindow : EditorWindow
	{

        private static EditorWindow window;
        private static Texture2D windowIcon;

        [MenuItem(Constants.SETTINGS_WINDOW_PATH, false, Constants.SETTINGS_WINDOW_PRIORITY)]
        private static void ShowWindow()
        {
            Init();
            window.Show();
        }

	    private static void Init()
	    {
            if (window == null)
                window = GetWindow(typeof(SettingsWindow));

            if (windowIcon == null)
                windowIcon = Base64.FromBase64(Constants.SETTINGS_WINDOW_ICON);

            window.minSize = Constants.SETTINGS_WINDOW_MIN_SIZE;

#if UNITY_5_4_OR_NEWER
            window.titleContent = new GUIContent(Constants.SETTINGS_WINDOW_NAME, windowIcon);
#else
            window.title = Constants.SETTINGS_WINDOW_NAME;
#endif
        }


	    void OnGUI()
	    {
            Init();

            GUILayout.Space(10);
            EditorGUILayout.LabelField(Constants.SETTINGS_CONTENT_TITLE, (GUIStyle)"BoldLabel");
            GUILayout.Space(15);

            EditorGUI.BeginChangeCheck();
            using (new GUILayout.HorizontalScope())
            {
                GUILayout.Label(Constants.SETTINGS_SHOWINSPECTOR, GUILayout.ExpandWidth(true));
                Settings.ShowCustomInspector = EditorGUILayout.Toggle(Settings.ShowCustomInspector, GUILayout.ExpandWidth(true));
            }
            using (new GUILayout.HorizontalScope())
            {
                GUILayout.Label(Constants.SETTINGS_STAYPERSISTENT, GUILayout.ExpandWidth(true));
                Settings.ComponentsStayPersistent = EditorGUILayout.Toggle(Settings.ComponentsStayPersistent, GUILayout.ExpandWidth(true));
            }
	        if (EditorGUI.EndChangeCheck())
	        {
                Settings.Save();
	        }

            GUILayout.FlexibleSpace();

	        if (GUILayout.Button(Constants.SETTINGS_BUTTON_CLOSE))
	        {
	            window.Close();
	        }
        }

    }
}