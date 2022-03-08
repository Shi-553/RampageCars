using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    public class HitAudio : MonoBehaviour
    {
        public AudioClip hit,explosion;
        AudioSource audioSource;
        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void PlayExplosion()
        {
            audioSource.PlayOneShot(explosion);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name=="Capsule")
            {
                audioSource.PlayOneShot(hit);
                Invoke("PlayExplosion", 1);
            }
            if (collision.gameObject.name == "TestEnemy(Clone)")
            {
                audioSource.PlayOneShot(hit);
                Invoke("PlayExplosion", 1);
            }
        }
    }
}
