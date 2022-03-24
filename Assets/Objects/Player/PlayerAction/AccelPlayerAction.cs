﻿using System.Collections;
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
        public bool IsPlaying { get; private set; }

        Rigidbody rb;
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }
        private void FixedUpdate()
        {
            if (IsPlaying)
            {
                rb.AddForce((transform.forward * speed), ForceMode.Force);
            }
        }

        IEnumerator WaitFinish()
        {
            yield return new WaitForSeconds(duration);

            IsPlaying = false;
        }

        public void Do()
        {
            IsPlaying = true;
            StartCoroutine(WaitFinish());
        }

        public void Finish()
        {
        }
        public void OnCollisionEnter(Collision collision)
        {
            var damageable = collision.gameObject.GetComponent<IPublishable<CollisionDamageInfo>>();
            if (damageable is not null and IEnemyTag)
            {
                damageable.Publish(new(attack, collision));
            }
        }
    }
}
