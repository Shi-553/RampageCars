using UnityEngine;
using UnityEditor;

namespace EditorScripts
{
    // 参考
    // https://hacchi-man.hatenablog.com/entry/2021/01/01/220000
    [CustomPropertyDrawer(typeof(NotNullAttribute))]
    public class NotNullDrawer : PropertyDrawer
    {
        enum RequireType
        {
            OK,
            Require,
            NotSupport
        }
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property, label, true);
            position.y += EditorGUI.GetPropertyHeight(property);
            position.height = base.GetPropertyHeight(property, label);


            switch (IsRequire(property))
            {
                case RequireType.Require:
                    EditorGUI.HelpBox(position, "Set Value", MessageType.Error);
                    break;
                case RequireType.NotSupport:
                    EditorGUI.HelpBox(position, "Not Support", MessageType.Error);
                    Debug.LogError("サポート外のNotNullがあります", property.serializedObject.targetObject);
                    break;
                default:
                    break;
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (IsRequire(property) != RequireType.OK)
            {
                return EditorGUI.GetPropertyHeight(property) + base.GetPropertyHeight(property, label);
            }
            return EditorGUI.GetPropertyHeight(property);
        }

        private RequireType IsRequire(SerializedProperty property)
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.ObjectReference:
                    return property.objectReferenceValue == null ? RequireType.Require : RequireType.OK;

                default:
                    return RequireType.NotSupport;
            }
        }
    }
}
