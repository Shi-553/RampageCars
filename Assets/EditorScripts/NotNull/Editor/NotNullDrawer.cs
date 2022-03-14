using UnityEngine;
using System.Reflection;
using EditorScripts;

namespace UnityEditor
{

    [InitializeOnLoad]
    public static class DisallowPlayModeIfNull
    {
        static DisallowPlayModeIfNull()
        {
            EditorApplication.playModeStateChanged += LogNullError;
        }

        private static void LogNullError(PlayModeStateChange state)
        {
            if (state != PlayModeStateChange.ExitingEditMode)
            {
                return;
            }

            bool isError = false;

            foreach (var component in GameObject.FindObjectsOfType<Component>(true))
            {
                foreach (var field in component.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
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
                        var className = ObjectNames.GetClassName(component);

                        string message = $"<b>{component.name}</b> in <b>{className}</b> in <b>{fieldName}</b> is null!";

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

    [CustomPropertyDrawer(typeof(NotNullAttribute))]
    public class NotNullDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position.height = base.GetPropertyHeight(property, label);
            EditorGUI.PropertyField(position, property, label);
            position.y += position.height;
            if (IsRequire(property))
            {
                EditorGUI.HelpBox(position, "Set Value", MessageType.Error);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (IsRequire(property))
            {
                return base.GetPropertyHeight(property, label) * 2f;
            }
            return base.GetPropertyHeight(property, label);
        }

        private bool IsRequire(SerializedProperty property)
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.ObjectReference:
                    return property.objectReferenceValue == null;
            }

            return false;
        }
    }
}
