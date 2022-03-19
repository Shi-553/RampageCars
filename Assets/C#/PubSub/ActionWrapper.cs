using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RampageCars
{
    public class ActionWrapper<T0> : ISubscribeable<T0>, IPublishable<T0> where T0 : IInfo
    {
        Action<T0> action;
        public ActionWrapper() { }
        public ActionWrapper(Action<T0> action) { this.action = action; }

        /// <returns>UnScribeアクション</returns>
        public Action Subscribe(Action<T0> add)
        {
            action += add;
            return () => action -= add;
        }
        public void Publish(T0 arg0) => action?.Invoke(arg0);
    }
}
