using System;

namespace RampageCars
{
    public interface ISubscribeable<T0>
    {
        /// <returns>UnScribeアクション</returns>
        public Action Subscribe(Action<T0> add);
    }
}
