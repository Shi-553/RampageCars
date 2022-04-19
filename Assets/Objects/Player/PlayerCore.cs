using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RampageCars
{
    public class PlayerCore : MonoBehaviour, IPlayerTag, ISubscribeable<CollisionDamageInfo>, IPublishable<CollisionDamageInfo>, ISubscribeable<DeathInfo>
    {
        [SerializeField]
        private float healthPoint = 30;

        [SerializeField]
        float destroyDelayTime = 1;
        public bool IsDeath => healthPoint <= 0;

        public float CurrentHP;
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
            GetComponent<Rigidbody>().AddForce(info.fixedImpulse, ForceMode.Impulse);

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
            Destroy(gameObject);
        }

        void Start()
        {
            slider.value = 1;
            CurrentHP = healthPoint;
        }

        private void OnTriggerEnter(Collider collider)
        {
            if(collider.gameObject.tag == "Enemy")
            {
                float Damage = 1.0f;

                CurrentHP -= Damage;
                slider.value = CurrentHP / healthPoint;

            }
        }
    }
}
