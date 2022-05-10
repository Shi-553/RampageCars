using System.Collections;
using System.Collections.Generic;
using EditorScripts;
using UnityEngine;

namespace RampageCars
{
    public class ContactEffect : MonoEventReceiver
    {
        [SerializeField, NotNull]
        GameObject hit;
        [SerializeField, NotNull]
        GameObject explosion;

        private void Start()
        {
            OnDestroyed += GetComponent<ISubscribeable<CollisionDamageInfo>>().Subscribe(OnDamage);

            OnDestroyed += GetComponent<ISubscribeable<DelayDestroyInfo>>().Subscribe(OnDeath);
        }
        private void OnDamage(CollisionDamageInfo info)
        {
            Instantiate(hit, transform.position, Quaternion.identity);

        }
        private void OnDeath(DelayDestroyInfo n)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
        }
    }
}
