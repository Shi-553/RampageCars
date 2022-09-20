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
        [SerializeField,Tooltip("ダイブ中に敵にあたったときの攻撃力")]
        float attack = 3;
        [SerializeField, Tooltip("衝撃波の攻撃力")]
        float diveAttack = 10;
        [SerializeField, Tooltip("衝撃波の吹っ飛ばす力")]
        float impulseScale = 5;
        [SerializeField]
        float range = 5;
        [SerializeField]
        Vector3 diveSpeed = new(0, 1, 1);
        public bool CanChange { get; private set; } = true;

        [SerializeField, NotNull]
        GameObject rangeObj;
        private GameObject obj;
        Rigidbody rb;
        Animator animator;
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            animator = GetComponentInChildren<Animator>();
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

            animator.SetTrigger("Dive");

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

            Collider[] targets = Physics.OverlapSphere(this.transform.position, range);
            foreach (Collider colliders in targets)
            {
                var damageable = colliders.GetComponent<IPublishable<CollisionDamageInfo>>();
                if (damageable is IEnemyTag)
                {
                    var impulse = (colliders.transform.position - obj.transform.position).normalized * impulseScale;
                    damageable.Publish(new(diveAttack, impulse));
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
            if (damageable is IEnemyTag)
            {
                damageable.Publish(new(attack, collision.impulse));
            }
        }
    }
}
