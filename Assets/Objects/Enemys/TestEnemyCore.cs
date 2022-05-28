using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RampageCars
{
    public class TestEnemyCore : MonoBehaviour, IEnemyTag, ICollisionPawn, IFallSeaPawn
    {
        [SerializeField]
        float healthPointMax = 6;
        [SerializeField]
        float healthPoint;
        public float HealthPoint { get => healthPoint; set => healthPoint = value; }
        public float HealthPointMax => healthPointMax;

        ActionWrapper<HPPercentInfo> IPubSub<HPPercentInfo>.PubSubAction { get; } = new();

        ActionWrapper<DeathInfo> IPubSub<DeathInfo>.PubSubAction { get; } = new();
        ActionWrapper<DamageInfo> IPubSub<DamageInfo>.PubSubAction { get; } = new();
        ActionWrapper<CollisionDamageInfo> IPubSub<CollisionDamageInfo>.PubSubAction { get; } = new();
        ActionWrapper<FallSeaInfo> IPubSub<FallSeaInfo>.PubSubAction { get; } = new();


        void Awake()
        {
            healthPoint = healthPointMax;
        }
    }
}
