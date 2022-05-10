using System;

namespace RampageCars
{
    public interface IFallSeaPawn : IPawn, ISubscribeableImpl<FallSeaInfo>, IPublishableImpl<FallSeaInfo>
    {
        // 海に落ちた
        void IPublishable<FallSeaInfo>.Publish(FallSeaInfo info)
        {
            if (IsDeath)
                return;
            Publish(new DamageInfo(info.damage));

            DefaultPublish(info);
        }
    }
}
