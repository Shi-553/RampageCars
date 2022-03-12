using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RampageCars
{
    public class DeathInfo:IInfo
    {
    }

    public class TestEnemy : MonoBehaviour, IEnemyTag, ISubscribeable<ICollisionDamageInfo>, IPublishable<ICollisionDamageInfo>, ISubscribeable<DeathInfo>
    {
        [SerializeField]
        private float healthPoint = 100;

        [SerializeField]
        float destroyDelayTime = 1;
        public bool IsDeath => healthPoint <= 0;


        ActionWrapper<DeathInfo> onDeath = new();
        public Action Subscribe(Action<DeathInfo> add) => onDeath.Subscribe(add);


        ActionWrapper<ICollisionDamageInfo> onDamage = new();
        public Action Subscribe(Action<ICollisionDamageInfo> add) => onDamage.Subscribe(add);

        public void Publish(ICollisionDamageInfo info)
        {
            if (IsDeath)
            {
                return;
            }

            var f = info.Collision.impulse;
            f.y += 1;

            GetComponent<Rigidbody>().AddForce(f, ForceMode.Impulse);

            onDamage?.Publish(info);
            healthPoint -= info.Value;

            if (IsDeath)
            {
                StartCoroutine(DelayDestroy());
            }
        }

        IEnumerator DelayDestroy()
        {
            yield return new WaitForSeconds(destroyDelayTime);

            onDeath?.Publish(new());
            Destroy(gameObject);
        }

    }
}
