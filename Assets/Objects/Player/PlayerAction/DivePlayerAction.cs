using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    public class DivePlayerAction : MonoBehaviour, IPlayerAction
    {
        [SerializeField]
        GroundChecker groundChecker;
        [SerializeField]
        float attack = 3;
        [SerializeField]
        float diveAttack = 10;
        [SerializeField]
        float range = 5;
        public bool CanChange { get; private set; } = true;
        public bool isFirst { get; private set; } = true;

        public GameObject rangeObj;
        private GameObject obj;
        Rigidbody rb;
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }
        private void FixedUpdate()
        {
            if (!CanChange)
            {
                if (groundChecker.IsGrounded)
                {
                    obj = Instantiate(rangeObj, this.transform.position, Quaternion.identity);
                    obj.transform.localScale = new Vector3(range * 2, range * 2, range * 2);
                    Collider[] targets = Physics.OverlapSphere(this.transform.position, range);
                    foreach (Collider colliders in targets)
                    {
                        var damageable = colliders.gameObject.GetComponent<IPublishable<CollisionDamageInfo>>();
                        if (damageable is not null and IEnemyTag)
                        {
                            damageable.Publish(new (diveAttack, null, new Vector3(0.0f, 0.0f, 0.0f)));
                        }
                    }
                    CanChange = true;
                }
            }
        }

        public void Do()
        {
            if (!groundChecker.IsGrounded)
            {
                CanChange = false;
                Destroy(obj);
            }
        }

        public void Finish()
        {
        }
        public void OnCollisionEnter(Collision collision)
        {
            var damageable = collision.gameObject.GetComponent<IPublishable<CollisionDamageInfo>>();
            if (damageable is not null and IEnemyTag)
            {
                damageable.Publish(new (attack, collision));
            }
        }
    }
}
