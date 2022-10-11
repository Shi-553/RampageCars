using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    public class AccelPlayerAction : MonoBehaviour, IPlayerAction
    {

        [SerializeField]
        float attack = 10;
        [SerializeField]
        float speed = 100;
        [SerializeField]
        float duration = 1;
        public bool CanChange { get; private set; } = true;

        Rigidbody rb;
        Animator animator;

        [SerializeField]
        GameObject accelEffect;
        [SerializeField]
        Transform accelEffectParent;

        private ParticleSystem accelParticleSystem;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            animator = GetComponentInChildren<Animator>();
        }
        private void FixedUpdate()
        {
            if (!CanChange)
            {
                rb.AddForce((transform.forward * speed), ForceMode.Force);
            }
        }


        public void Do()
        {
            CanChange = false;
            animator.SetBool("IsAccel",true);

            if (accelParticleSystem == null)
            {
                accelParticleSystem=Instantiate(accelEffect, accelEffectParent).GetComponent<ParticleSystem>();
            }
            accelParticleSystem.Play();
        }

        public void Finish()
        {
            animator.SetBool("IsAccel", false);
            CanChange = true;
            accelParticleSystem.Stop();
        }
        public void CollisionEnter(Collision collision)
        {
            var damageable = collision.collider.GetComponent<IPublishable<CollisionDamageInfo>>();
            if (damageable is IEnemyTag)
            {
                damageable.Publish(new(attack, collision.impulse));
            }
        }
    }
}
