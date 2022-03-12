using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{

    public class ContactEffect : MonoEventReceiver
    {
        [SerializeField]
        GameObject hit;
        [SerializeField]
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
