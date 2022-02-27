using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DragDrop {
    public class DragDropDrawerBase : PropertyDrawer {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            var isDammy = fieldInfo.FieldType == typeof(DammyNextDragDropProperty);
            return isDammy ? 0 : EditorGUI.GetPropertyHeight(property);
        }

        //D&D
        protected List<Object> GetDroppedObject(Rect rect, bool includeSubAsset = false) {
            List<Object> list = new();

            //�}�E�X�̈ʒu��D&D�͈̔͂ɂȂ���΃X���[
            if (!rect.Contains(Event.current.mousePosition)) {
                return list;
            }

            //���݂̃C�x���g���擾
            EventType eventType = Event.current.type;

            //�h���b�O���h���b�v�ő��삪 �X�V���ꂽ�Ƃ� or ���s�����Ƃ�
            if (eventType == EventType.DragUpdated || eventType == EventType.DragPerform) {
                //�J�[�\����+�̃A�C�R����\��
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                //�h���b�v���ꂽ�I�u�W�F�N�g�����X�g�ɓo�^
                if (eventType == EventType.DragPerform) {
                    if (includeSubAsset) {
                        foreach (var path in DragAndDrop.paths) {
                            list.AddRange(AssetDatabase.LoadAllAssetsAtPath(path));
                        }
                    }
                    else {
                        list = new(DragAndDrop.objectReferences);
                    }
                    //�h���b�O���󂯕t����(�h���b�O���ăJ�[�\���ɂ����t���Ă��I�u�W�F�N�g���߂�Ȃ��Ȃ�)
                    DragAndDrop.AcceptDrag();
                }

                //�C�x���g���g�p�ς݂ɂ���
                Event.current.Use();
            }

            return list;
        }

        public static string GetPropertyType(SerializedProperty property) {
            var type = property.type;
            var match = Regex.Match(type, @"PPtr<\$(.*?)>");
            return match.Success ? match.Groups[1].Value : type;
        }


        public static bool IsArrayOrList(SerializedProperty property) {
            return property.isArray && property.propertyType != SerializedPropertyType.String;
        }
    }


    [CustomPropertyDrawer(typeof(SubclassFromOtherDragDropAttribute))]
    public class SubclassFromOtherDragDropDrawer : DragDropDrawerBase {

        bool SetElement(SerializedProperty element, System.Type subclass, System.Type dropType, Object dropObject) {
            try {
                if (element.managedReferenceValue == null) {
                    element.managedReferenceValue = System.Activator.CreateInstance(subclass);
                }

                foreach (var child in element) {
                    var childSerializedProperty = child as SerializedProperty;
                    if (childSerializedProperty == null || GetPropertyType(childSerializedProperty) != dropType.Name) {
                        continue;
                    }

                    if (childSerializedProperty.objectReferenceValue == null) {
                        childSerializedProperty.objectReferenceValue = dropObject;
                    }
                    break;
                }

                return true;
            }
            catch (System.InvalidOperationException e) {
                Debug.LogWarning(e);
                return false;
            }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            var isDammy = fieldInfo.FieldType == typeof(DammyNextDragDropProperty);

            if (isDammy) {
                property.Next(false);
                position.height = EditorGUI.GetPropertyHeight(property, false);
            }
            else {
                EditorGUI.PropertyField(position, property, label, true);
            }

            var attributes = fieldInfo.GetCustomAttributes(typeof(SubclassFromOtherDragDropAttribute), true);
            var dropObjects = GetDroppedObject(position, true);


            foreach (var dropObject in dropObjects) {
                var dropObjectType = dropObject.GetType();

                foreach (var att in attributes) {
                    var subclassDragDropAttribute = att as SubclassFromOtherDragDropAttribute;

                    if (subclassDragDropAttribute == null || subclassDragDropAttribute.DropedType != dropObjectType) {
                        continue;
                    }

                    if (IsArrayOrList(property)) {
                        var index = property.arraySize;
                        property.InsertArrayElementAtIndex(index);
                        var element = property.GetArrayElementAtIndex(index);

                        if (!SetElement(element, subclassDragDropAttribute.Subclass, dropObjectType, dropObject)) {
                            property.DeleteArrayElementAtIndex(index);
                            continue;
                        }

                        break;
                    }

                    if (SetElement(property, subclassDragDropAttribute.Subclass, dropObjectType, dropObject)) {
                        return;
                    }
                }
            }
        }
    }


    [CustomPropertyDrawer(typeof(PathDragDropAttribute))]
    public class PathDragDropDrawer : DragDropDrawerBase {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            var isDammy = fieldInfo.FieldType == typeof(DammyNextDragDropProperty);

            if (isDammy) {
                property.Next(false);
                position.height = EditorGUI.GetPropertyHeight(property);
            }


            var dropObjectList = GetDroppedObject(position);

            if (property.propertyType == SerializedPropertyType.String) {

                if (dropObjectList.Count > 0) {
                    property.stringValue = AssetDatabase.GetAssetPath(dropObjectList[0]);
                }
            }

            if (IsArrayOrList(property) && property.arrayElementType == "string") {
                foreach (var draggedObject in dropObjectList) {
                    var index = property.arraySize;
                    property.InsertArrayElementAtIndex(index);
                    var element = property.GetArrayElementAtIndex(index);

                    element.stringValue = AssetDatabase.GetAssetPath(draggedObject);

                }
            }

            if (!isDammy) {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }

    }
}