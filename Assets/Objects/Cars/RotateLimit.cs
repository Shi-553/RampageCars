using System.Collections;
using System.Collections.Generic;
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
        float limitedAngularDrag = 5.0f;

        Quaternion initialRotation = Quaternion.identity;
        bool isLimited = false;

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
            if (isLimited)
            {
                Debug.Log("limited!" + (rb.velocity * -limitedDrag));
                //Drag
                rb.AddForce(rb.velocity * -limitedDrag);
                rb.AddTorque(rb.angularVelocity * -limitedAngularDrag);
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
