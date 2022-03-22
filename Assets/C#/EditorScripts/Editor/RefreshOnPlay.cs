using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

namespace EditorScripts
{
    [InitializeOnLoad]
    public static class RefreshOnPlay
    {
        private const string LOCK_MENU_PATH = "Tools/RefreshOnPlay/Lock Compile";
        private const string FORCE_MENU_PATH = "Tools/RefreshOnPlay/Force Compile #%r";

        private static bool isLocked = false;

        static RefreshOnPlay()
        {
            EditorApplication.playModeStateChanged += PlayRefresh;
        }

        private static void PlayRefresh(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingEditMode)
            {
                ForceCompile();
            }
            if (state == PlayModeStateChange.EnteredEditMode)
            {
                Lock();
            }
        }

        [MenuItem(LOCK_MENU_PATH, false, 1)]
        static void SwitchLock()
        {
            if (isLocked)
            {
                Unlock();
            }
            else
            {
                Lock();
            }
        }

        [MenuItem(FORCE_MENU_PATH)]
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
            isLocked = true;

            Menu.SetChecked(LOCK_MENU_PATH, isLocked);
        }

        private static void Unlock()
        {
            Debug.Log("<b>Unlock</b> Reload Assemblies");

            EditorApplication.UnlockReloadAssemblies();
            isLocked = false;

            Menu.SetChecked(LOCK_MENU_PATH, isLocked);
        }
    }
}
