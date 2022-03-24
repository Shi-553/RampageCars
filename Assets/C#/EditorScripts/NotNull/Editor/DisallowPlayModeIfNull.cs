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


        static IEnumerable<SerializedProperty> GetFields(Component obj)
        {
            var serializedObject = new SerializedObject(obj);

            var property = serializedObject.GetIterator();

            while (property.NextVisible(true))
            {
                if (property.propertyType == SerializedPropertyType.ObjectReference)
                {
                    yield return property;
                }
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
                foreach (var field in GetFields(component))
                {
                    // 真のnullと偽装nullで2重チェック
                    if ((System.Object)field.objectReferenceValue != null && ((UnityEngine.Object)field.objectReferenceValue) != null)
                    {
                        continue;
                    }

                    var attribute = field.GetFieldInfo()?.GetCustomAttribute<NotNullAttribute>(true);

                    if (attribute is null)
                    {
                        continue;
                    }

                    isError = true;

                    var name = attribute.IsWrapper ? field.propertyPath.Split(".")[^2] : field.name;

                    var fieldName = ObjectNames.NicifyVariableName(name);
                    var className = ObjectNames.NicifyVariableName(component.GetType().Name);

                    string message = $"<b>{component.name}</b> -> <b>{className}</b> -> <b>{fieldName}</b> is null!";


                    Debug.unityLogger.logHandler.LogFormat(LogType.Error, component, message);
                }
            }

            if (isError)
            {
                EditorApplication.ExitPlaymode();
            }
        }
    }
}
