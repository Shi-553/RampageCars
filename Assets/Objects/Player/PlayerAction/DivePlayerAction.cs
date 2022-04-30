using System.Collections;
using System.Collections.Generic;
using EditorScripts;
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
        [SerializeField]
        Vector3 diveSpeed = new(0, 1, 1);
        public bool CanChange { get; private set; } = true;

        [SerializeField, NotNull]
        GameObject rangeObj;
        private GameObject obj;
        Rigidbody rb;
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }
        private void FixedUpdate()
        {
        }

        public void Do()
        {
            if (!groundChecker.IsGrounded)
            {
                StartCoroutine(Dive());
            }
        }

        IEnumerator Dive()
        {
            CanChange = false;
            Destroy(obj);

            rb.AddForce(transform.forward * diveSpeed.z + Vector3.down * diveSpeed.y, ForceMode.Impulse);

            while (true)
            {
                if (groundChecker.IsGrounded)
                {
                    break;
                }
                if (CanChange)
                {
                    yield break;
                }

                yield return null;
            }

            obj = Instantiate(rangeObj, this.transform.position, Quaternion.identity);
            obj.transform.localScale = new Vector3(range * 2, range * 2, range * 2);
            Collider[] targets = Physics.OverlapSphere(this.transform.position, range);
            foreach (Collider colliders in targets)
            {
                var damageable = colliders.GetComponent<IPublishable<CollisionDamageInfo>>();
                if (damageable is not null and IEnemyTag)
                {
                    var impulse = (colliders.transform.position - obj.transform.position).normalized * 2;
                    damageable.Publish(new(diveAttack, null, impulse));
                }
            }
            CanChange = true;
        }

        public void Finish()
        {
        }
        public void CollisionEnter(Collision collision)
        {
            var damageable = collision.gameObject.GetComponent<IPublishable<CollisionDamageInfo>>();
            if (damageable is not null and IEnemyTag)
            {
                damageable.Publish(new(attack, collision));
            }
        }
    }
}
