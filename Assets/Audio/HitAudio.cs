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

        AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();

            OnDestroyed += GetComponent<IAffectable<ICollisionDamageInfo>>().OnAffect.Subscribe(OnDamage);

            OnDestroyed += GetComponent<IDeathable>().OnDeath.Subscribe(OnDeath);
        }

        private void OnDamage(ICollisionDamageInfo info)
        {
            audioSource.PlayOneShot(hit);
        }
        private void OnDeath()
        {
            audioSource.PlayOneShot(explosion);
        }

    }
}
