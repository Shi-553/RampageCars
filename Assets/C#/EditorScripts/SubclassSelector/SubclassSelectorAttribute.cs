using System;
using UnityEngine;

namespace EditorScripts
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class SubclassSelectorAttribute : PropertyAttribute
    {
        private bool m_includeMono;

        public SubclassSelectorAttribute(bool includeMono = false)
        {
            m_includeMono = includeMono;
        }

        public bool IsIncludeMono()
        {
            return m_includeMono;
        }
    }
}
