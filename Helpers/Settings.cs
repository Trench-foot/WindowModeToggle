using BepInEx.Configuration;
using UnityEngine;

namespace WindowModeToggle.Helpers
{
    internal enum ScreenMode
    {
        ExclusiveFullScreen,
        FullScreenWindow,
        MaximizedWindow,
        Windowed
    }
    internal enum ModifierKeyOptions
    {
        Alt,
        Shift,
        Control
    }

    internal class Settings
    {
        private const string MainScreenModeSetting = "A. Main window mode.";
        private const string AltScreenModeSetting = "B. Secondary window mode";
        private const string WindowToggleKeySetting = "C. Window toggle mode key";
        private const string ModifierKeySettings = "D. Toggle to require use of modifier key";

        public static ConfigEntry<ScreenMode> MainScreenModeToggle { get; set; }
        public static ConfigEntry<ScreenMode> AltScreenModeToggle { get; set; }
        public static ConfigEntry<KeyboardShortcut> WindowToggleKey;
        public static ConfigEntry<bool> ModifierKeyToggle { get; set; }
        public static ConfigEntry<ModifierKeyOptions> ModifierKey;


        public static void Init(ConfigFile Config)
        {
            MainScreenModeToggle = Config.Bind(
                MainScreenModeSetting,
                "Primary screen mode",
                ScreenMode.Windowed,
                "Screen mode setting number 1."
            );

            AltScreenModeToggle = Config.Bind(
                AltScreenModeSetting,
                "Secondary screen mode",
                ScreenMode.MaximizedWindow,
                "Screen mode setting number 2."
            );

            WindowToggleKey = Config.Bind(
                WindowToggleKeySetting,
                "1. Toggle Window mode",
                new KeyboardShortcut(KeyCode.F9),
                "Key to toggle between window modes dynamically."
            );

            ModifierKeyToggle = Config.Bind(
                ModifierKeySettings,
                "2. Toggle usage of a modifier key",
                false,
                "Key to modify activation of custom hotkeys."
            );

            ModifierKey = Config.Bind(
                ModifierKeySettings,
                "3. Modifier key",
                ModifierKeyOptions.Alt,
                "Hold this key to use with toggle key."
            );
        }
    }
}
