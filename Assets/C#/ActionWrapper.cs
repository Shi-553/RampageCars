using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RampageCars
{
    public class ActionWrapper
    {
        UnityAction action;
        public ActionWrapper() {  }
        public ActionWrapper(UnityAction action) { this.action = action; }

        /// <returns>UnScribeアクション</returns>
        public UnityAction Subscribe(UnityAction add)
        {
            action += add;
            return () => action -= add;
        }
        public void Invoke() => action?.Invoke();
    }

    public class ActionWrapper<T0>
    {
        UnityAction<T0> action;
        public ActionWrapper() { }
        public ActionWrapper(UnityAction<T0> action) { this.action = action; }

        /// <returns>UnScribeアクション</returns>
        public UnityAction Subscribe(UnityAction<T0> add)
        {
            action += add;
            return () => action -= add;
        }
        public void Invoke(T0 arg0) => action?.Invoke(arg0);
    }
    public class ActionWrapper<T0, T1>
    {
        UnityAction<T0, T1> action;
        public ActionWrapper() { }
        public ActionWrapper(UnityAction<T0, T1> action) { this.action = action; }

        /// <returns>UnScribeアクション</returns>
        public UnityAction Subscribe(UnityAction<T0, T1> add)
        {
            action += add;
            return () => action -= add;
        }
        public void Invoke(T0 arg0, T1 arg1) => action?.Invoke(arg0, arg1);
    }
    public class ActionWrapper<T0, T1, T2>
    {
        UnityAction<T0, T1, T2> action;
        public ActionWrapper() { }
        public ActionWrapper(UnityAction<T0, T1, T2> action) { this.action = action; }

        /// <returns>UnScribeアクション</returns>
        public UnityAction Subscribe(UnityAction<T0, T1, T2> add)
        {
            action += add;
            return () => action -= add;
        }
        public void Invoke(T0 arg0, T1 arg1, T2 arg2) => action?.Invoke(arg0, arg1, arg2);
    }
    public class ActionWrapper<T0, T1, T2, T3>
    {
        UnityAction<T0, T1, T2, T3> action;
        public ActionWrapper() { }
        public ActionWrapper(UnityAction<T0, T1, T2, T3> action) { this.action = action; }

        /// <returns>UnScribeアクション</returns>
        public UnityAction Subscribe(UnityAction<T0, T1, T2, T3> add)
        {
            action += add;
            return () => action -= add;
        }
        public void Invoke(T0 arg0, T1 arg1, T2 arg2, T3 arg3) => action?.Invoke(arg0, arg1, arg2, arg3);
    }
}
