using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace RampageCars
{
    public class PlayerCore : MonoBehaviour, IPlayerTag, ICollisionPawn, IFallSeaPawn
    {
        [SerializeField]
        float healthPointMax;
        [SerializeField]
        Slider slider;
        public float HealthPoint { get; set; }
        ActionWrapper<DeathInfo> IPubSub<DeathInfo>.PubSubAction { get; } = new();
        ActionWrapper<DamageInfo> IPubSub<DamageInfo>.PubSubAction { get; } = new();
        ActionWrapper<CollisionDamageInfo> IPubSub<CollisionDamageInfo>.PubSubAction { get; } = new();
        ActionWrapper<FallSeaInfo> IPubSub<FallSeaInfo>.PubSubAction { get; } = new();

        void Awake()
        {
            HealthPoint = healthPointMax;

            slider.maxValue = 1;
            slider.value = 1;
            this.StaticCast<ISubscribeable<DamageInfo>>().Subscribe((_) => slider.value = HealthPoint / healthPointMax);

            this.StaticCast<ISubscribeable<DeathInfo>>().Subscribe((_) => Singleton.Get<SceneLoader>().ChangeScene(SceneType.GameOver));
        }
    }
}
