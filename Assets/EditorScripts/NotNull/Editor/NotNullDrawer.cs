using UnityEngine;
using UnityEditor;

namespace EditorScripts
{
    // 参考
    // https://hacchi-man.hatenablog.com/entry/2021/01/01/220000
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
