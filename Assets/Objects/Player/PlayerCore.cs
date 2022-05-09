using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace RampageCars
{
    public class PlayerCore : MonoBehaviour, IPlayerTag, ISubscribeable<CollisionDamageInfo>, IPublishable<CollisionDamageInfo>, ISubscribeable<DeathInfo>, IPublishable<FallSeaInfo>, ISubscribeable<FallSeaInfo>
    {
        [SerializeField]
        private float healthPointMax = 30;

        [SerializeField]
        float destroyDelayTime = 1;
        public bool IsDeath => healthPoint <= 0;

        public float healthPoint;
        public Slider slider;

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
            slider.value = healthPoint / healthPointMax;

            if (IsDeath)
            {
                StartCoroutine(DelayDestroy());
                var loader = Singleton.Get<SceneLoader>();
                loader.ChangeScene(SceneType.GameOver);
            }



        }

        IEnumerator DelayDestroy()
        {
            yield return new WaitForSeconds(destroyDelayTime);

            onDeath?.Publish(new());
            Destroy(gameObject);
        }

        void Start()
        {
            slider.value = 1;
            healthPoint = healthPointMax;
        }


        ActionWrapper<FallSeaInfo> onFallSea = new();
        public void Publish(FallSeaInfo info) => onFallSea.Publish(info);
        public Action Subscribe(Action<FallSeaInfo> add) => onFallSea.Subscribe(add);
    }
}
