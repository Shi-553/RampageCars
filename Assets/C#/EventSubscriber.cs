using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RampageCars
{
    public class MonoEventReceiver : MonoBehaviour
    {
       public event UnityAction OnDestroyed;

        protected virtual void OnDestroy()
        {
            OnDestroyed?.Invoke();
        }
    }
}
