using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RampageCars
{
    public class MonoEventReceiver : MonoBehaviour
    {
        public event Action OnDestroyed;
        protected virtual void OnDestroy()
        {
            OnDestroyed?.Invoke();
        }
    }
}
