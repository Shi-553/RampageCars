using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    public enum CollisionTiming
    {
        Enter,
        Exit
    }
    public readonly struct TriggerReceiveInfo
    {
        public readonly CollisionTiming timing;
        public readonly Collider body;

        public TriggerReceiveInfo(CollisionTiming timing, Collider body)
        {
            this.timing = timing;
            this.body = body;
        }
    }
    public class ColliderTriggerReceiver : MonoBehaviour, ISubscribeableImpl<TriggerReceiveInfo>
    {
        public ActionWrapper<TriggerReceiveInfo> PubSubAction { get; } = new();


        private void OnTriggerEnter(Collider other)
        {
            PubSubAction.Publish(new(CollisionTiming.Enter, other));
        }
        private void OnTriggerExit(Collider other)
        {
            PubSubAction.Publish(new(CollisionTiming.Exit, other));
        }
    }
}
