using System;
using System.Collections.Generic;
using UnityEngine;

namespace DragDrop {
    /// <summary>
    /// ���̎��̃v���p�e�B��Ώۂɂ���
    /// </summary>
    [Serializable]
    public class DammyNextDragDropProperty {
    }

    /// <summary>
    /// �ʂ̒l����h���b�N&�h���b�v��SerializeReference��ݒ肷�邽�߂̑���
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class SubclassFromOtherDragDropAttribute : PropertyAttribute {
        public Type Subclass { get; private set; }
        public Type DropedType { get; private set; }

        public SubclassFromOtherDragDropAttribute(Type subclass, Type dropedType) {
            Subclass = subclass;
            DropedType = dropedType;
        }
    }


    /// <summary>
    /// �h���b�N&�h���b�v�Ńp�X��ݒ肷�邽�߂̑���
    /// </summary>
    public class PathDragDropAttribute : PropertyAttribute {
    }
}