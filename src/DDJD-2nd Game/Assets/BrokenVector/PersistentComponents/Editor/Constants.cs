using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace BrokenVector.PersistentComponents
{
    public static class Constants
    {
        private const string TOOL_MENU_PATH = "Tools/";
        private const string BRAND_NAME = "BrokenVector";
        private const string WINDOW_MENU_PATH = "Window/" + BRAND_NAME + "/";
        
        public const string ASSET_NAME = "Persistent Components";
        public const string SETTINGS_NAME = "Settings";

        public const string PREF_NAME = BRAND_NAME + ".PCS";

        public const string WINDOW_PATH = TOOL_MENU_PATH + ASSET_NAME + "/Open";
        public const string WINDOW_PATH_ALTERNATE = WINDOW_MENU_PATH + ASSET_NAME;
        public const string SETTINGS_WINDOW_PATH = TOOL_MENU_PATH + ASSET_NAME + "/" + SETTINGS_NAME;

        public const int WINDOW_PRIORITY = 10;
        public const int SETTINGS_WINDOW_PRIORITY = 20;

        public const string WINDOW_NAME = " PCS";
        public const string SETTINGS_WINDOW_NAME = " Settings";

        public const string WINDOW_ICON = "iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAu0lEQVQ4ja3TMWoCQRgF4C9iuXiAVCkkaYMHEK32HFbWaQIpU4Wk8QJW3mJaDyBbW20V7NdWsMgUy7Kzrmwe/DC8/703/wwzDMQDhBAqZJErsUvo57E+8jz/gVFsZDVRic9aVXiM6330fIcQ3usBXXjBrIX/6huQwgjGPYTrrmZbwAKXu8YYguYEbyhueF6xSQUUOOA5YT42icFH+Pc7gIn2hwOnPgG/2HZsOm0LOPv7DyssO8zwVPMMxxU8zRu9p8JebQAAAABJRU5ErkJggg==";
        public const string SETTINGS_WINDOW_ICON = "iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAABf0lEQVQ4jXXTMWsUURQF4C/rIiFIkBBFUcRCFCGIsRHERkQPpLARG1vtFCLY2PgDBGsbFUGsRCSizQPFLoWC2thICosYUgQJIkuKsFjMjIyz62kG7jn3zH3n3Tehg1JKHwtYwXe8xioWk2x29RNjDB7iOj5gGbdq6jjOYivJsxGDUspFfMNt3Owa4zem0MOVJC+gXzfP4VVN7hzTDLvq7xbeNsV+q7iOw/9pbmMS70opU5jfUUqZrEdbwIGWcIAneI9DmG5x+zGLr31cxePOX4a4kGS5PuI9fMbBluYRlnqYGTPmp6YZkmzgeUezFyd6Se7jXIecNopu7RLu9kopM7jWIY+WUpr7V0o5ozpqG6v4OFEb/FCl28UKfuGk6oobbGJPku1ekp+q5bmhWpY2juBUpxl2446GSPJAleramCnaGKpWfIA3fw1qzKq2cICX2B5jsJTkNI4l+fKPQZI1zON8ksuqkGBRFfJQlYckDTf6GhvUj2sOT7GBfUnWu7o/fEhpUO+QfAEAAAAASUVORK5CYII=";

        public const float ICON_SIZE = 18;

        public const string CONTENT_TITLE = ASSET_NAME;
        public const string SETTINGS_CONTENT_TITLE = CONTENT_TITLE + " - Settings";

        public const string PREFS_SHOWCUSTOMINSPECTOR = "ShowCustomInspector";
        public const string PREFS_COMPSSTAYPERSISTENT = "ComponentsStayPersistent";

        public const string ASSET_SAVEDATA_NAME = "WatchedComponents.asset";

        public const string CONTEXTMENU_TOGGLESTRING = "Toggle Persistent";

        public const string SETTINGS_SHOWINSPECTOR = "show custom inspector";
        public const string SETTINGS_STAYPERSISTENT = "components stay persistent";
        public const string SETTINGS_BUTTON_CLOSE = "Close";

        public const string PCWINDOW_NOTINPLAYMODENOTICE = "To mark Components as persistent while not in playmode, activate 'components stay persistent' in the settings";
        public const string PCWINDOW_BUTTON_ADDALL = "Add everything";
        public const string PCWINDOW_BUTTON_REMALL = "Remove everything";

        public static readonly Vector2 WINDOW_MIN_SIZE = new Vector2(230, 170);
        public static readonly Vector2 SETTINGS_WINDOW_MIN_SIZE = new Vector2(230, 0);
    }
}