using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

namespace EditorScripts
{
    [InitializeOnLoad]
    public static class RefreshOnPlay
    {
        private const string LOCK_MENU_PATH = "Tools/RefreshOnPlay/Lock Compile";
        private const string FORCE_MENU_PATH = "Tools/RefreshOnPlay/Force Compile %r";

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
                UnLock();
            }
            else
            {
                Lock();
            }
        }

        [MenuItem(FORCE_MENU_PATH)]
        private static void ForceCompile()
        {
            UnLock();

            AssetDatabase.Refresh();
            CompilationPipeline.RequestScriptCompilation();
        }

        private static void Lock()
        {
            Debug.Log("Lock Reload Assemblies");

            EditorApplication.LockReloadAssemblies();
            isLocked = true;

            Menu.SetChecked(LOCK_MENU_PATH, isLocked);
        }

        private static void UnLock()
        {
            Debug.Log("Unlock Reload Assemblies");

            EditorApplication.UnlockReloadAssemblies();
            isLocked = false;

            Menu.SetChecked(LOCK_MENU_PATH, isLocked);
        }
    }
}
