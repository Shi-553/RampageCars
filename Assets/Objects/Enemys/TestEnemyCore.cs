using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RampageCars
{
    public class TestEnemyCore : MonoBehaviour, IEnemyTag, ISubscribeable<CollisionDamageInfo>, IPublishable<CollisionDamageInfo>, ISubscribeable<DeathInfo>, IPublishable<FallSeaInfo>, ISubscribeable<FallSeaInfo>
    {
        [SerializeField]
        private float healthPoint = 100;

        [SerializeField]
        float destroyDelayTime = 1;
        public bool IsDeath => healthPoint <= 0;


        ActionWrapper<DeathInfo> onDeath = new();
        public Action Subscribe(Action<DeathInfo> add) => onDeath.Subscribe(add);


        ActionWrapper<CollisionDamageInfo> onDamage = new();
        public Action Subscribe(Action<CollisionDamageInfo> add) => onDamage.Subscribe(add);

        public void Publish(CollisionDamageInfo info)
        {
            if (IsDeath)
            {
                return;
            }
            GetComponentInParent<Rigidbody>().AddForce(info.fixedImpulse, ForceMode.Impulse);

            onDamage?.Publish(info);
            healthPoint -= info.damage;

            if (IsDeath)
            {
                StartCoroutine(DelayDestroy());
            }
        }

        IEnumerator DelayDestroy()
        {
            yield return new WaitForSeconds(destroyDelayTime);

            onDeath?.Publish(new());
            Destroy(transform.parent.gameObject);
        }


        ActionWrapper<FallSeaInfo> onFallSea = new();
        public void Publish(FallSeaInfo info) => onFallSea.Publish(info);
        public Action Subscribe(Action<FallSeaInfo> add) => onFallSea.Subscribe(add);
    }
}
