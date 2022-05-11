using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    public readonly struct CollisionDamageInfo
    {
        public readonly float damage;
        public readonly Vector3 fixedImpulse;
        public CollisionDamageInfo(float damage, Vector3 fixedImpulse)
        {
            this.damage = damage;
            this.fixedImpulse = fixedImpulse;
        }

    }
}
