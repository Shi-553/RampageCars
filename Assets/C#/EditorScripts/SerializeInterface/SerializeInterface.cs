using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace EditorScripts
{
    [System.Serializable]
    public class SerializeInterface<T>
    {
        [SerializeField,NotNull(true)]
        Component obj;
        public T Interface
        {
            get
            {
                if (obj is T t)
                {
                    return t;
                }
                return default;
            }
        }
    }
}
