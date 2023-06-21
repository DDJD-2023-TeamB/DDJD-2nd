using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BrokenVector.PersistentComponents.Utils;
using UnityEngine;
using UnityEditor;

namespace BrokenVector.PersistentComponents
{
	public class PersistentComponentsWindow : EditorWindow
	{
        public static PersistentComponentsWindow Instance { private set; get; }
	    private static readonly GUIContent removeButtonContent = new GUIContent("X");
	    private static EditorWindow window;

	    private static Texture2D windowIcon;
        private static Texture2D removeIcon;

        private string searchText = "";

        private PersistentComponentsWindow()
        {
            Instance = this;
        }

        [MenuItem(Constants.WINDOW_PATH, false, Constants.WINDOW_PRIORITY), MenuItem(Constants.WINDOW_PATH_ALTERNATE)]
        public static void ShowWindow()
        {
            Init();
            window.Show();
        }

	    private static void Init()
	    {
            if (window == null)
                window = GetWindow(typeof(PersistentComponentsWindow));

            if (windowIcon == null)
                windowIcon = Base64.FromBase64(Constants.WINDOW_ICON);

            if (removeIcon == null)
                removeIcon = EditorGUIUtility.FindTexture("winbtn_mac_close_a");

            window.minSize = Constants.WINDOW_MIN_SIZE;

#if UNITY_5_4_OR_NEWER
            window.titleContent = new GUIContent(Constants.WINDOW_NAME, windowIcon);
#else
            window.title = Constants.WINDOW_NAME;
#endif
        }

	    private Vector2 currentScrollPos = Vector2.zero;
        void OnGUI()
        {
            Init();

            GUILayout.Space(10);
            EditorGUILayout.LabelField(Constants.CONTENT_TITLE, (GUIStyle)"BoldLabel");
            GUILayout.Space(5);

            if (Application.isPlaying || Settings.ComponentsStayPersistent)
                DrawComponentList();
            else
                GUILayout.TextArea(Constants.PCWINDOW_NOTINPLAYMODENOTICE);

            HandleDrop();
        }

        private void HandleDrop()
        {
            var currentEvent = Event.current;
            var currentEventType = currentEvent.type;

            if(currentEventType == EventType.DragUpdated)
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Link;
                DragAndDrop.AcceptDrag();
            }
            if(currentEventType == EventType.DragPerform)
            {
                foreach(var obj in DragAndDrop.objectReferences)
                {
                    if (obj is Component)
                        PersistentComponents.Instance.WatchComponent(obj as Component);
                    else if (obj is GameObject)
                        PersistentComponents.Instance.WatchComponents((obj as GameObject).GetComponents<Component>());
                }
            }
        }

        private void DrawAddRemButtons()
        {
            using (new GUILayout.HorizontalScope())
            {
                if (GUILayout.Button(Constants.PCWINDOW_BUTTON_ADDALL))
                    PersistentComponents.Instance.WatchComponents(FindObjectsOfType<Component>());
                if (GUILayout.Button(Constants.PCWINDOW_BUTTON_REMALL))
                    PersistentComponents.Instance.ForgetEveryComponent();
            }
        }

        private void DrawSearchBar()
        {
            searchText = SearchUtils.BeginSearchbar(this, searchText);
            SearchUtils.EndSearchbar();
        }

	    private void DrawComponentList()
	    {
            DrawAddRemButtons();
            GUILayout.Space(10);
            DrawSearchBar();

            using (new GUILayout.HorizontalScope((GUIStyle)"ShurikenEffectBg"))
            {
                using (var scrollView = new GUILayout.ScrollViewScope(currentScrollPos, false, false))
                {
                    var toRemove = new List<Component>();
                    foreach (var go in PersistentComponents.Instance.WatchedComponents)
                    {
                        if (go.Key == null)
                            continue;

                        List<Component> filteredComponents = new List<Component>();
                        List<Component> objComponents = new List<Component>();

                        foreach (var component in go.Value)
                        {
                            var instance = (Component)EditorUtility.InstanceIDToObject(component);
                            objComponents.Add(instance);
                            if (SearchUtils.IsSearched(instance, searchText))
                                filteredComponents.Add(instance);
                        }

                        if (filteredComponents.Count == 0)
                            continue;

                        DrawGameObject(go.Key, toRemove, objComponents);

                        foreach (var component in filteredComponents)
                        {
                            DrawComponent(component, toRemove);
                        }
                    }

                    foreach (var removeable in toRemove)
                    {
                        PersistentComponents.Instance.ForgetComponent(removeable);
                    }

                    currentScrollPos = scrollView.scrollPosition;
                }
            }
        }

        private static void DrawGameObject(GameObject go, List<Component> toRemove, List<Component> objComponents)
        {
            using (new GUILayout.HorizontalScope())
            {
                GUILayout.Label(go.name, GUILayout.ExpandWidth(false));

                var alignmentBckp = GUIStyle.none.alignment;
                GUIStyle.none.alignment = TextAnchor.MiddleCenter;
                if (GUILayout.Button(removeIcon, GUIStyle.none, GUILayout.Width(Constants.ICON_SIZE), GUILayout.Height(Constants.ICON_SIZE)))
                    toRemove.AddRange(objComponents);
                GUIStyle.none.alignment = alignmentBckp;
            }
        }

        private static void DrawComponent(Component component, List<Component> toRemove)
        {
            if (component == null)
                return;

            using (new GUILayout.HorizontalScope((GUIStyle)"HelpBox"))
            {
                Vector2 size = GUIStyle.none.CalcSize(removeButtonContent);
                float inspectorWidth = EditorGUIUtility.currentViewWidth;
                float buttonWidth = inspectorWidth - size.x - Constants.ICON_SIZE * 2 - 40; // 40 because of offsets and scrollbar

                var content = EditorGUIUtility.ObjectContent(null, component.GetType()).image;
                if (content == null)
                    content = EditorGUIUtility.IconContent("cs Script Icon").image;
                GUILayout.Label(content, GUILayout.MaxWidth(Constants.ICON_SIZE),
                    GUILayout.MaxHeight(Constants.ICON_SIZE));

                if (GUILayout.Button(component.GetType().Name,
                    GUILayout.MaxWidth(buttonWidth), GUILayout.Width(buttonWidth),
                    GUILayout.ExpandWidth(false)))
                {
                    EditorGUIUtility.PingObject(component);
                    Selection.activeGameObject = component.gameObject;
                }

                var alignmentBckp = GUIStyle.none.alignment;
                GUIStyle.none.alignment = TextAnchor.MiddleCenter;
                if (GUILayout.Button(removeIcon, GUIStyle.none, GUILayout.Width(Constants.ICON_SIZE), GUILayout.Height(Constants.ICON_SIZE)))
                    toRemove.Add(component);
                GUIStyle.none.alignment = alignmentBckp;
            }
        }

    }
}