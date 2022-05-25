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
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }
        private void FixedUpdate()
        {
            if (!CanChange)
            {
                rb.AddForce((transform.forward * speed), ForceMode.Force);
            }
        }

        IEnumerator WaitFinish()
        {
            yield return new WaitForSeconds(duration);

            CanChange = true;
        }

        public void Do()
        {
            CanChange = false;
            StartCoroutine(WaitFinish());
        }

        public void Finish()
        {
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
