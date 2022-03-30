using System.Collections;
using System.Collections.Generic;
using EditorScripts;
using UnityEngine;

namespace RampageCars
{
    public class RotateLimit : MonoBehaviour
    {
        [SerializeField]
        float limit = 50;
        [Header("limitで抑えているときの速度と角速度の抵抗力")]
        [SerializeField]
        float limitedDrag = 3.0f;
        [SerializeField]
        float dampenFactor = 0.8f;
        [SerializeField]
        float adjustFactor = 0.5f; 

        Quaternion initialRotation = Quaternion.identity;
        bool isLimited = false;
        [SerializeField,NotNull]
        GroundChecker groundChecker;
        Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }
        void Update()
        {
            var newRot = transform.localRotation;
            var initRot = initialRotation;

            initRot.eulerAngles = new(initRot.eulerAngles.x, newRot.eulerAngles.y, initRot.eulerAngles.z);

            var clampedRot = ClampRotation(initRot, newRot, limit);

            isLimited = clampedRot != newRot;

            transform.localRotation = clampedRot;
        }

        private void FixedUpdate()
        {
            if (isLimited && groundChecker.IsGrounded)
            {
                Debug.Log("limited!" + (rb.velocity * -limitedDrag));
                //Drag
                rb.AddForce(rb.velocity * -limitedDrag);

                // https://stackoverflow.com/questions/58419942/stabilize-hovercraft-rigidbody-upright-using-torque/58420316#58420316
                var deltaQuat = Quaternion.FromToRotation(transform.up, Vector3.up);

                deltaQuat.ToAngleAxis(out float angle, out Vector3 axis);

                rb.AddTorque(-rb.angularVelocity * dampenFactor, ForceMode.Acceleration);

                rb.AddTorque(adjustFactor * angle * axis.normalized, ForceMode.Acceleration);
            }
        }

        // Clamps "b" such that it never exceeds "maxAngle" degrees from "a"
        Quaternion ClampRotation(Quaternion a, Quaternion b, float maxAngle)
        {
            float newAngle = Quaternion.Angle(a, b);
            if (newAngle <= maxAngle)
            {
                // Rotation within allowable constraint
                return b;
            }
            else
            {
                // This is the proportion of the new rotation that is within the constraint
                float angleRatio = maxAngle / newAngle;
                return Quaternion.Slerp(a, b, angleRatio);
            }
        }
    }
}
