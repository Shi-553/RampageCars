using System;
using UnityEngine;

namespace RampageCars
{
    public abstract class ReceiverBase<T> : MonoBehaviour, ISubscribeable<T>
    {
        protected ActionWrapper<T> actionWrapper = new();
        public Action Subscribe(Action<T> add) => actionWrapper.Subscribe(add);
    }
}
