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
            OnDestroyed += GetComponent<IAffectable<ICollisionDamageInfo>>().OnAffect.Subscribe(OnDamage);

            OnDestroyed += GetComponent<IDeathable>().OnDeath.Subscribe(OnDeath);
        }
        private void OnDamage(ICollisionDamageInfo info)
        {
            Instantiate(hit, transform.position, Quaternion.identity);
        }
        private void OnDeath()
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
        }
    }
}
