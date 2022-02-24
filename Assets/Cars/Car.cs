using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    public class Car : MonoBehaviour
    {
        [SerializeField]
        List<Axle> axles;

        [SerializeField]
        float speedMax = 100;
        [SerializeField]
        float brakeSpeedMax= 100;
        [SerializeField]
        float steeringMax = 100;

        Rigidbody rigid;
        public float Velocity { private set; get; }
        public float VelocityForward { private set; get; }

        private void Awake()
        {
            rigid = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            Velocity = rigid.velocity.magnitude;
            VelocityForward = Vector3.Dot(rigid.velocity, transform.forward);
        }

        public void SetMotorTorque(float motorTorque)
        {
            foreach (var axle in axles)
            {
                axle.SetMotorTorque(motorTorque* speedMax);
            }
        }
        public void SetBrakeTorque(float brakeTorque)
        {
            foreach (var axle in axles)
            {
                axle.SetBrakeTorque(brakeTorque * brakeSpeedMax);
            }
        }
        public void SetSteerAngle(float steerAngle)
        {
            foreach (var axle in axles)
            {
                axle.SetSteerAngle(steerAngle* steeringMax);
            }
        }
    }
}