using UnityEngine;
using System;

namespace EditorScripts
{
    [AttributeUsage(AttributeTargets.Field)]
    public class NotNullAttribute : PropertyAttribute
    {
    }
}
