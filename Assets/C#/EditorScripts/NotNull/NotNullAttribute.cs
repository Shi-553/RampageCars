using UnityEngine;
using System;

namespace EditorScripts
{
    [AttributeUsage(AttributeTargets.Field)]
    public class NotNullAttribute : PropertyAttribute
    {
        public bool IsWrapper { get; private set; }
        public NotNullAttribute(bool isWrapper = false)
        {
            IsWrapper = isWrapper;
        }
    }
}
