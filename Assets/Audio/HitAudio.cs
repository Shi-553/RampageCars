using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    public class HitAudio : MonoEventReceiver
    {
        [SerializeField]
        AudioClip hit;
        [SerializeField]
        AudioClip explosion;


        private void Start()
        {
            OnDestroyed += GetComponent<ISubscribeable<ICollisionDamageInfo>>().Subscribe(OnDamage);

            OnDestroyed += GetComponent<ISubscribeable<DeathInfo>>().Subscribe(OnDeath);
        }

        private void OnDamage(ICollisionDamageInfo info)
        {
            Singleton.Get<SEManager>().Play(hit, transform.position, 1);
        }
        private void OnDeath(DeathInfo n)
        {
            Singleton.Get<SEManager>().Play(explosion, transform.position, 1);
        }

    }
}
