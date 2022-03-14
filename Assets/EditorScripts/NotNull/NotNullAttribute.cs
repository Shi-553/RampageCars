using UnityEngine;
using System;

namespace EditorScripts
{
    // https://hacchi-man.hatenablog.com/entry/2021/01/01/220000
    [AttributeUsage(AttributeTargets.Field)]
    public class NotNullAttribute : PropertyAttribute
    {
    }
}
