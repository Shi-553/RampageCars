using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    public class DefaultPlayerAction : MonoBehaviour, IPlayerAction
    {
        [SerializeField]
        int normalDamage = 3;
        public bool CanChange => true;

        public void Do() { }
        public void Finish() { }
        public void CollisionEnter(Collision collision)
        {

            var damageable = collision.collider.GetComponent<IPublishable<CollisionDamageInfo>>();
            if (damageable is IEnemyTag)
            {
                damageable.Publish(new(normalDamage,  collision.impulse * 0.5f));
            }
        }
    }
}
