using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    public class RealCar : MonoBehaviour, ICar
    {
        [SerializeField]
        List<Axle> axles;

        [SerializeField]
        float speedMax = 100;
        [SerializeField]
        float brakeSpeedMax= 100;
        [SerializeField]
        float steeringMax = 100;

        Rigidbody rb;

        Vector3 boost;
        void Awake()
        {
            rb = GetComponent<Rigidbody>();

        }
        private void FixedUpdate()
        {
            var v = rb.velocity;
            v.y = -1;
            v += boost;
            rb.velocity = v;


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

        public void Boost(Vector3 v)
        {
            boost = v;
        }

        public void Jamp()
        {
        }
    }
}