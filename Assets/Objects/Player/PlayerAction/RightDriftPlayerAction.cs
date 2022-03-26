using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EditorScripts;

namespace RampageCars
{
    public class RightDriftPlayerAction : MonoBehaviour, IPlayerAction
    {
        [SerializeField, NotNull]
        GroundChecker groundChecker;
        [SerializeField]
        float addRelative;
        Rigidbody rb;

        [SerializeField]
        float rotatePower = 10;
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
                    rb.AddRelativeForce(new Vector3(addRelative, addRelative, addRelative));

                    rb.MoveRotation(transform.localRotation * Quaternion.AngleAxis(rotatePower, Vector3.up));
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
