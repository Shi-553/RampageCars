using System;

namespace RampageCars
{
    // 当たることができる
    public interface ICollisionPawn : IPawn, IPubSubImpl<CollisionDamageInfo>
    {
        // 衝突ダメージ
        void IPublishable<CollisionDamageInfo>.Publish(CollisionDamageInfo info)
        {
            if (IsDeath)
                return;

            Publish(new DamageInfo(info.damage));

            DefaultPublish(info);
        }
    }
}
