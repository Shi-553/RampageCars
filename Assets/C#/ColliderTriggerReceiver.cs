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
    public readonly struct CollisionReceiveInfo
    {
        public readonly CollisionTiming timing;
        public readonly Collider body;

        public CollisionReceiveInfo(CollisionTiming timing, Collider body)
        {
            this.timing = timing;
            this.body = body;
        }
    }
    public class ColliderTriggerReceiver :MonoBehaviour, ISubscribeable<CollisionReceiveInfo>
    {
        public ActionWrapper<CollisionReceiveInfo> PubSubAction { get; init; } =new();

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
