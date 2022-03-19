using System;
using UnityEngine;

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
