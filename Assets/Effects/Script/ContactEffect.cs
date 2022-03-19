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
            OnDestroyed += GetComponent<ISubscribeable<ICollisionDamageInfo>>().Subscribe(OnDamage);

            OnDestroyed += GetComponent<ISubscribeable<DeathInfo>>().Subscribe(OnDeath);
        }
        private void OnDamage(ICollisionDamageInfo info)
        {
            Instantiate(hit, transform.position, Quaternion.identity);

        }
        private void OnDeath(DeathInfo n)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
        }
    }
}
