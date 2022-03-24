using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    public readonly struct CollisionDamageInfo
    {
        public readonly float damage;
        public readonly Vector3 fixedImpulse;
        public readonly Collision collision;
        public CollisionDamageInfo(float damage,  Collision collision, Vector3 fixedImpulse)
        {
            this.damage = damage;
            this.collision = collision;
            this.fixedImpulse = fixedImpulse;
        }
        public CollisionDamageInfo(float damage,  Collision collision)
        {
            this.damage = damage;
            this.collision = collision;
            this.fixedImpulse = collision.impulse;
        }
    }

}
