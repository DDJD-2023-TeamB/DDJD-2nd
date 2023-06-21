using System.Collections;
using System.Collections.Generic;
using BrokenVector.PersistentComponents.Utils;
using UnityEngine;
using UnityEditor;

namespace BrokenVector.PersistentComponents
{
    [InitializeOnLoad]
	public class Settings : Editor
	{

        private static readonly AssetPrefs prefs = new AssetPrefs(Constants.PREF_NAME);

        public static bool ShowCustomInspector { get; set; }
        public static bool ComponentsStayPersistent { get; set; }

        static Settings()
        {
            Load();
        }

	    public static void Load()
	    {
	        ShowCustomInspector = prefs.Get(Constants.PREFS_SHOWCUSTOMINSPECTOR, true);
            ComponentsStayPersistent = prefs.Get(Constants.PREFS_COMPSSTAYPERSISTENT, true);
	    }

	    public static void Save()
	    {
	        prefs.Set(Constants.PREFS_SHOWCUSTOMINSPECTOR, ShowCustomInspector);
            prefs.Set(Constants.PREFS_COMPSSTAYPERSISTENT, ComponentsStayPersistent);
	    }

	}
}