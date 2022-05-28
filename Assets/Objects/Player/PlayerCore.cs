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
        public float HealthPointMax => healthPointMax;
        [SerializeField]
        float healthPoint;
        public float HealthPoint { get => healthPoint; set => healthPoint = value; }

        ActionWrapper<HPPercentInfo> IPubSub<HPPercentInfo>.PubSubAction { get; } = new();
        ActionWrapper<DeathInfo> IPubSub<DeathInfo>.PubSubAction { get; } = new();
        ActionWrapper<DamageInfo> IPubSub<DamageInfo>.PubSubAction { get; } = new();
        ActionWrapper<CollisionDamageInfo> IPubSub<CollisionDamageInfo>.PubSubAction { get; } = new();
        ActionWrapper<FallSeaInfo> IPubSub<FallSeaInfo>.PubSubAction { get; } = new();

        void Awake()
        {
            healthPoint = healthPointMax;

            this.StaticCast<ISubscribeable<DeathInfo>>()
                .Subscribe((_) => Singleton.Get<GameRoleManager>().GameOver());

        }
    }
}
