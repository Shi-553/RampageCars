using System.Collections;
using EditorScripts;
using UnityEngine;

namespace RampageCars
{
    public abstract class DriftPlayerActionBase : MonoBehaviour, IPlayerAction
    {
        [SerializeField, NotNull]
        GroundChecker groundChecker;
        [SerializeField]
        float attack = 3;

        [Header("抵抗力と回転力")]
        [SerializeField]
        float addRelative = 100;

        public abstract float RotatePower { get; }

        Rigidbody rb;
        public bool CanChange { get; private set; } = true;

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
                    rb.AddRelativeForce(new Vector3(-addRelative, -addRelative, -addRelative));

                    rb.MoveRotation(transform.localRotation * Quaternion.AngleAxis(RotatePower, Vector3.up));
                }
            }
        }


        public void Do()
        {
            CanChange = false;
        }

        public void Finish()
        {
            CanChange = true;
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
