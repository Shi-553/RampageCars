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
    public class ColliderTriggerReceiver : ReceiverBase<CollisionReceiveInfo>
    {
        private void OnTriggerEnter(Collider other)
        {
            actionWrapper.Publish(new(CollisionTiming.Enter, other));
        }
        private void OnTriggerExit(Collider other)
        {
            actionWrapper.Publish(new(CollisionTiming.Exit, other));
        }
    }
}
