using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    public interface ICollisionDamageInfo : IDamageInfo
    {
        public Collision Collision { get; }
    }

    [System.Serializable]
    public class CollisionDamageInfo : DamageInfo, ICollisionDamageInfo
    {
        public Collision Collision { get; set; }
    }
}
