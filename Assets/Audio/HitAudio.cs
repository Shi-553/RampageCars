using System.Collections;
using System.Collections.Generic;
using EditorScripts;
using UnityEngine;

namespace RampageCars
{
    public class HitAudio : MonoEventReceiver
    {
        [SerializeField, NotNull]
        AudioClip hit;
        [SerializeField, NotNull]
        AudioClip explosion;


        private void Start()
        {
            OnDestroyed += GetComponent<ISubscribeable<CollisionDamageInfo>>().Subscribe(OnDamage);

            OnDestroyed += GetComponent<ISubscribeable<DeathInfo>>().Subscribe(OnDeath);
        }

        private void OnDamage(CollisionDamageInfo info)
        {
            Singleton.Get<SEManager>().Play(hit, transform.position, 1);
        }
        private void OnDeath(DeathInfo n)
        {
            Singleton.Get<SEManager>().Play(explosion, transform.position, 1);
        }

    }
}
