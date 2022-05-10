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
        public float HealthPoint { get; set; }

        ActionWrapper<DeathInfo> IPubSub<DeathInfo>.PubSubAction { get; init; } = new();
        ActionWrapper<DamageInfo> IPubSub<DamageInfo>.PubSubAction { get; init; } = new();
        ActionWrapper<CollisionDamageInfo> IPubSub<CollisionDamageInfo>.PubSubAction { get; init; } = new();
        ActionWrapper<FallSeaInfo> IPubSub<FallSeaInfo>.PubSubAction { get; init; } = new();


        void Awake()
        {
            HealthPoint = healthPointMax;
        }
    }
}
