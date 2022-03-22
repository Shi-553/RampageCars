using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

namespace EditorScripts
{
    [InitializeOnLoad]
    public static class RefreshOnPlay
    {
        private const string USE_AUTO_LOCK_PATH = "Tools/RefreshOnPlay/Use Auto Lock";
        private const string IS_LOCK_PATH = "Tools/RefreshOnPlay/Lock Compile";
        private const string FORCE_COMPILE_PATH = "Tools/RefreshOnPlay/Force Compile #%r";

        private static bool UseAutoLock
        {
            get => Menu.GetChecked(USE_AUTO_LOCK_PATH);
            set => Menu.SetChecked(USE_AUTO_LOCK_PATH, value);
        }
        private static bool IsLocked
        {
            get => Menu.GetChecked(IS_LOCK_PATH);
            set => Menu.SetChecked(IS_LOCK_PATH, value);
        }

        static RefreshOnPlay()
        {
            EditorApplication.playModeStateChanged += PlayRefresh;
        }

        private static void PlayRefresh(PlayModeStateChange state)
        {
            if (!UseAutoLock)
            {
                return;
            }

            if (state == PlayModeStateChange.ExitingEditMode)
            {
                ForceCompile();
            }
            if (state == PlayModeStateChange.EnteredEditMode)
            {
                Lock();
            }
        }

        [MenuItem(USE_AUTO_LOCK_PATH, true)]
        private static bool SwitchUseAutoLockValidaste()
        {
            try
            {
                UseAutoLock = System.Convert.ToBoolean(EditorUserSettings.GetConfigValue(USE_AUTO_LOCK_PATH));
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e.Message);
                UseAutoLock = false;
            }
            return true;
        }

        [MenuItem(USE_AUTO_LOCK_PATH, priority = 0)]
        private static void SwitchUseAutoLock()
        {
            UseAutoLock = !UseAutoLock;

            EditorUserSettings.SetConfigValue(USE_AUTO_LOCK_PATH, UseAutoLock.ToString());
            AssetDatabase.SaveAssets();
        }

        [MenuItem(IS_LOCK_PATH, priority = 20)]
        static void SwitchLock()
        {
            if (IsLocked)
            {
                Unlock();
            }
            else
            {
                Lock();
            }
        }

        [MenuItem(FORCE_COMPILE_PATH, priority = 21)]
        private static void ForceCompile()
        {
            Unlock();

            AssetDatabase.Refresh();
            CompilationPipeline.RequestScriptCompilation();
        }

        private static void Lock()
        {
            Debug.Log("<b>Lock</b> Reload Assemblies");

            EditorApplication.LockReloadAssemblies();
            IsLocked = true;
        }

        private static void Unlock()
        {
            Debug.Log("<b>Unlock</b> Reload Assemblies");

            EditorApplication.UnlockReloadAssemblies();
            IsLocked = false;
        }
    }
}
