using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace BrokenVector.PersistentComponents
{
    public partial class PersistentComponents
    {
        [MenuItem("GameObject/" + Constants.CONTEXTMENU_TOGGLESTRING, false, 0)]
        public static void TogglePersistentGO(MenuCommand command)
        {
            if (Selection.transforms.Length == 0)
            {
                GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
                foreach (GameObject go in allObjects)
                    if (go.activeInHierarchy)
                        ToggleGO(go);
            }
            else
            {
                ToggleGO(command.context as GameObject);
            }
        }

        [MenuItem("CONTEXT/Component/" + Constants.CONTEXTMENU_TOGGLESTRING, false, 50)]
        public static void TogglePersistent(MenuCommand command)
        {
            var target = (Component)command.context;
            if (Instance.IsComponentWatched(target))
            {
                Instance.ForgetComponent(target);
            }
            else
            {
                Instance.WatchComponent(target);
            }
        }

        private static void ToggleGO(GameObject go)
        {
            bool allWatched = true;
            foreach (var c in go.GetComponents<Component>())
                if (!Instance.IsComponentWatched(c))
                {
                    allWatched = false;
                    break;
                }

            if (!allWatched)
                Instance.WatchComponents(go.GetComponents<Component>());
            else
                Instance.ForgetComponents(go.GetComponents<Component>());
        }
    }
}