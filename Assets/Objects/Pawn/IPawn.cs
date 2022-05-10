using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RampageCars
{
    // HPを持ち、ダメージを受け、死ぬことができる
    public interface IPawn : ISubscribeable<DeathInfo>, ISubscribeable<DamageInfo>, IPublishable<DamageInfo>
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
