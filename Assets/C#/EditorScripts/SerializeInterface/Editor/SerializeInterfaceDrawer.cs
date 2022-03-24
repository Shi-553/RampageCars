using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace EditorScripts
{
    [CustomPropertyDrawer(typeof(SerializeInterface<>))]
    public class SerializeInterfaceDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.Next(true);
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
            property.Next(true);
            if (IsRequire(property))
            {
                return base.GetPropertyHeight(property, label) * 2f;
            }
            return base.GetPropertyHeight(property, label);
        }

        private bool IsRequire(SerializedProperty property)
        {
            if (property.propertyType != SerializedPropertyType.ObjectReference || property.objectReferenceValue == null)
            {
                return true;
            }

            var generic = fieldInfo.FieldType.GenericTypeArguments[0];

            List<Component> components = new();

            if (property.objectReferenceValue is Transform gameObject)
            {
                gameObject.GetComponents<Component>(components);
            }
            else
            {
                components.Add(property.objectReferenceValue as Component);
            }

            foreach (var o in components)
            {
                if (generic.IsAssignableFrom(o.GetType()))
                {
                    property.objectReferenceValue = o;
                    return false;
                }
            }

            property.objectReferenceValue = null;
            return true;
        }
    }
}
