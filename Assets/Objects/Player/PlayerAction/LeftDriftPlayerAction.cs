using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EditorScripts;

namespace RampageCars
{
    public class LeftDriftPlayerAction : MonoBehaviour, IPlayerAction
    {
        [SerializeField, NotNull]
        GroundChecker groundChecker;
        [Header("抵抗力と回転力")]
        [SerializeField]
        float addRelative=100;
        [SerializeField]
        float rotatePower = 5;
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

                    rb.MoveRotation(transform.localRotation * Quaternion.AngleAxis(-rotatePower, Vector3.up));
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
        }
    }
}
