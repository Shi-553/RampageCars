using System;

namespace RampageCars
{
    public interface IPubSub<T0>
    {
        ActionWrapper<T0> PubSubAction { get; init; }

        protected sealed void DefaultPublish(T0 info) => PubSubAction.Publish(info);

        /// <returns>UnScribeアクション</returns>
        protected sealed Action DefaultSubscribe(Action<T0> add) => PubSubAction.Subscribe(add);

    }

    public interface IPublishable<T0> : IPubSub<T0>
    {
        void Publish(T0 info) => DefaultPublish(info);
    }

    public interface ISubscribeable<T0> : IPubSub<T0>
    {
        /// <returns>UnScribeアクション</returns>
        Action Subscribe(Action<T0> add) => DefaultSubscribe(add);
    }
}
