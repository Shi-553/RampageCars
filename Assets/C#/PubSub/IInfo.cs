using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RampageCars
{
    public interface IInfo
    {
    }

    public interface ISubscribeable<T0> where T0 : IInfo
    {
        /// <returns>UnScribeアクション</returns>
        public Action Subscribe(Action<T0> add);
    }
    public interface IPublishable<T0> where T0 : IInfo
    {
        void Publish(T0 info);
    }
}
