using System;

namespace RampageCars
{
    // Pub/SubのためにActionWrapperプロパティの実装を強制する
    public interface IPubSub<T0>
    {
        ActionWrapper<T0> PubSubAction { get; }

        sealed void DefaultPublish(T0 info) => PubSubAction.Publish(info);

        /// <returns>UnScribeアクション</returns>
        sealed Action DefaultSubscribe(Action<T0> add) => PubSubAction.Subscribe(add);

    }


    // 外から検索して使う用

    public interface IPublishable<T0>
    {
        void Publish(T0 info);
    }

    public interface ISubscribeable<T0>
    {
        /// <returns>UnScribeアクション</returns>
        Action Subscribe(Action<T0> add);
    }


    // 実装時にはこれらを使うと便利

    public interface IPublishableImpl<T0> : IPublishable<T0>, IPubSub<T0>
    {
        void IPublishable<T0>.Publish(T0 info) => DefaultPublish(info);
    }

    public interface ISubscribeableImpl<T0> : ISubscribeable<T0>, IPubSub<T0>
    {
        /// <returns>UnScribeアクション</returns>
        Action ISubscribeable<T0>.Subscribe(Action<T0> add) => DefaultSubscribe(add);
    }
}
