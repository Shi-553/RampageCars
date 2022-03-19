using UnityEngine;
using System.Reflection;
using UnityEditor;
using System;
using System.Collections.Generic;

namespace EditorScripts
{
    [InitializeOnLoad]
    public static class DisallowPlayModeIfNull
    {
        static DisallowPlayModeIfNull()
        {
            EditorApplication.playModeStateChanged += LogNullError;
        }

        static IEnumerable<FieldInfo> GetFields(Type t)
        {
            if (t == null)
            {
                yield break;
            }

            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic |
                                 BindingFlags.Static | BindingFlags.Instance |
                                 BindingFlags.DeclaredOnly;
            foreach (var f in t.GetFields(flags))
            {
                yield return f;
            }
            foreach (var f in GetFields(t.BaseType))
            {
                yield return f;
            }
        }

        private static void LogNullError(PlayModeStateChange state)
        {
            if (state != PlayModeStateChange.EnteredPlayMode)
            {
                return;
            }

            bool isError = false;

            foreach (var component in GameObject.FindObjectsOfType<Component>(true))
            {
                foreach (var field in GetFields(component.GetType()))
                {
                    var attribute = field.GetCustomAttribute<NotNullAttribute>(true);

                    if (attribute is null)
                    {
                        continue;
                    }
                    var val = field.GetValue(component);

                    // 真のnullと偽装nullで2重チェック
                    if (val == null || (UnityEngine.Object)val == null)
                    {
                        isError = true;

                        var fieldName = ObjectNames.NicifyVariableName(field.Name);
                        var className = ObjectNames.NicifyVariableName(component.GetType().Name);

                        string message = $"<b>{component.name}</b> -> <b>{className}</b> -> <b>{fieldName}</b> is null!";

                        // こうするとダブルクリックしてもこの行に飛ばない
                        Debug.unityLogger.logHandler.LogFormat(LogType.Error, component, message);
                    }
                }
            }

            if (isError)
            {
                EditorApplication.ExitPlaymode();
            }
        }
    }
}
