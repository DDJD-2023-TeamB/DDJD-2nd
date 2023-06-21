using System;
using UnityEngine;
using UnityEditor;

namespace BrokenVector.PersistentComponents.Utils
{
    public static class SearchUtils
    {
        private const float CLEAR_BUTTON_WIDTH = 60f;

        private static GUISkin skin = null;
        private static GUIStyle toolbar;
        private static GUIStyle search;
        //private static GUIStyle button;
        private static GUIStyle cancelButton;


        static SearchUtils()
        {
            if (skin == null)
            {
                skin = EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector);
                toolbar = skin.FindStyle("Toolbar");
                search = skin.FindStyle("ToolbarSeachTextField");
                //button = skin.FindStyle("ToolbarButton");
                cancelButton = skin.FindStyle("ToolbarSeachCancelButton");
            }
        }

        public static string BeginSearchbar(EditorWindow window, string searchText)
        {
            GUILayout.BeginHorizontal(toolbar);

            searchText = GUILayout.TextField(searchText, search);
            if (GUILayout.Button("", cancelButton) || Event.current.keyCode == KeyCode.Escape)
            {
                searchText = "";
                GUI.FocusControl(null);
                window.Repaint();
            }

            return searchText;
        }

        public static void EndSearchbar()
        {
            GUILayout.EndHorizontal();
        }

        private static bool ContainsWord(string input, string containedWord)
        {
            var splitted = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var part in splitted)
            {
                if (containedWord.IndexOf(part, 0, StringComparison.OrdinalIgnoreCase) == -1)
                    return false;
            }
            return true;
        }

        private static bool FilterComponents(string searchQuery, GameObject go)
        {
            foreach (var component in go.GetComponents<Component>())
            {
                var type = component.GetType().Name;
                if (searchQuery.Equals(type, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }

        public static bool IsSearched(Component obj, string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
                return true;

            return string.IsNullOrEmpty(searchText) || ContainsWord(searchText, obj.GetType().Name);
        }
    }
}