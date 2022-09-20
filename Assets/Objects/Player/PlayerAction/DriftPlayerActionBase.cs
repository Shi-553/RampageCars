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
        Vector3 drag = new(1,0,1);

        public abstract float RotatePower { get; }
        public abstract string MotionName { get; }

        Rigidbody rb;
        Animator animator;
        public bool CanChange { get; private set; } = true;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();

            animator = GetComponentInChildren<Animator>();
        }
        private void FixedUpdate()
        {
            if (!CanChange)
            {
                if (groundChecker.IsGrounded)
                {
                    var localV = transform.InverseTransformDirection(rb.velocity);
                    float forceX = - drag.x * localV.x;
                    float forceY = - drag.y * localV.y;
                    float forceZ = - drag.z * localV.z;
                    rb.AddRelativeForce(new Vector3(forceX, forceY, forceZ));


                    rb.MoveRotation(transform.localRotation * Quaternion.AngleAxis(RotatePower, Vector3.up));
                }
            }
        }


        public void Do()
        {
            CanChange = false;
            animator.SetBool(MotionName,true);
        }

        public void Finish()
        {
            CanChange = true;
            animator.SetBool(MotionName, false);
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
