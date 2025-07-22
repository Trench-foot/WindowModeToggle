using BepInEx;
using BepInEx.Logging;
using UnityEngine;
using WindowModeToggle.Helpers;

namespace WindowModeToggle
{
    // first string below is your plugin's GUID, it MUST be unique to any other mod. Read more about it in BepInEx docs. Be sure to update it if you copy this project.
    [BepInPlugin("WindowModeToggle", "WindowModeToggle", "1.0.0")]
    [BepInDependency("com.SPT.core", "3.11.0")]
    public class Plugin : BaseUnityPlugin
    {
        public static ManualLogSource LogSource;
        // Test the current screen mode of tarkov
        private bool testScreenModeSettings()
        {
            FullScreenMode _fullScreenMode = GetFullScreenMode();
            
            if (_fullScreenMode == (FullScreenMode)Settings.MainScreenModeToggle.Value)
            {
                //Logger.LogInfo(_fullScreenMode + "false");
                return false;
            }
            else if (_fullScreenMode == (FullScreenMode)Settings.AltScreenModeToggle.Value)
            {
                //Logger.LogInfo(_fullScreenMode + "true");
                return true;
            }
            //Special use case, windows doesn't support Maximized window, but I couldn't just remove it
            // so this was born
            else if(_fullScreenMode == (FullScreenMode)ScreenMode.FullScreenWindow)
            {
                if(ScreenMode.MaximizedWindow == Settings.MainScreenModeToggle.Value)
                {
                    return false;
                }
                else if(ScreenMode.MaximizedWindow == Settings.AltScreenModeToggle.Value)
                {
                    return true;
                }
                return true;
            }
            else
            {
                //Logger.LogInfo(_fullScreenMode + "false");
                return false;
            }
        }
        // Test for Modifier key binding being presssed and maps the options to the 
        // left and right versions of it
        private bool testModifierKeyBind()
        {
            if(Settings.ModifierKey.Value == ModifierKeyOptions.Alt)
            {
                if(Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
                {
                    return true;
                }
            }
            else if(Settings.ModifierKey.Value == ModifierKeyOptions.Shift)
            {
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    return true;
                }
            }
            else if(Settings.ModifierKey.Value == ModifierKeyOptions.Control)
            {
                if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                {
                    return true;
                }
            }
            return false;
        }
        // Gets the current screen mode of tarkov
        private FullScreenMode GetFullScreenMode()
        {
            //Logger.LogInfo(Screen.fullScreenMode);
            return Screen.fullScreenMode;
        }
        // BaseUnityPlugin inherits MonoBehaviour, so you can use base unity functions like Awake() and Update()
        private void Awake()
        {
            Settings.Init(Config);
            LogSource = Logger;
            LogSource.LogInfo("plugin loaded!");
        }
        public void Update()
        {
            HandleInput();
        }
        // Handles checking for matching input and toggles the screen mode as per settings.
        private void HandleInput()
        {
            if (Settings.ModifierKeyToggle.Value)
            {
                if (testModifierKeyBind())
                {
                    if (Input.GetKeyDown(Settings.WindowToggleKey.Value.MainKey) && testScreenModeSettings())
                    {
                        Logger.LogInfo(GetFullScreenMode() + "false");
                        GameGraphicsClass.smethod_2((FullScreenMode)Settings.MainScreenModeToggle.Value);
                    }
                    else if (Input.GetKeyDown(Settings.WindowToggleKey.Value.MainKey) && !testScreenModeSettings())
                    {
                        Logger.LogInfo(GetFullScreenMode() + "true");
                        GameGraphicsClass.smethod_2((FullScreenMode)Settings.AltScreenModeToggle.Value);
                    }
                }
            }
            else if (!Settings.ModifierKeyToggle.Value)
            {
                if (Input.GetKeyDown(Settings.WindowToggleKey.Value.MainKey) && testScreenModeSettings())
                {
                    Logger.LogInfo(GetFullScreenMode() + "false");
                    GameGraphicsClass.smethod_2((FullScreenMode)Settings.MainScreenModeToggle.Value);
                }
                else if (Input.GetKeyDown(Settings.WindowToggleKey.Value.MainKey) && !testScreenModeSettings())
                {
                    Logger.LogInfo(GetFullScreenMode() + "true");
                    GameGraphicsClass.smethod_2((FullScreenMode)Settings.AltScreenModeToggle.Value);
                }
            }
        }
    }
}
