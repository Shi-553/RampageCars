using System;
using System.Collections.Generic;
using UnityEngine;

namespace DragDrop {
    /// <summary>
    /// この次のプロパティを対象にする
    /// </summary>
    [Serializable]
    public class DammyNextDragDropProperty {
    }

    /// <summary>
    /// 別の値からドラック&ドロップでSerializeReferenceを設定するための属性
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
    /// ドラック&ドロップでパスを設定するための属性
    /// </summary>
    public class PathDragDropAttribute : PropertyAttribute {
    }
}