

namespace RampageCars
{
    // HPを持ち、ダメージを受け、死ぬことができる
    public interface IPawn : ISubscribeableImpl<DeathInfo>, IPubSubImpl<DamageInfo>
    {
        float HealthPoint { get; set; }
        bool IsDeath => HealthPoint <= 0;

        
        void IPublishable<DamageInfo>.Publish(DamageInfo info)
        {
            if (IsDeath)
                return;

            HealthPoint -= info.damage;

            DefaultPublish(info);

            if (IsDeath)
                DefaultPublish(new DeathInfo());
        }
    }
}
