using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    public class Axle : MonoBehaviour
    {
        WheelCollider[] wheels=new WheelCollider[0];

        [SerializeField]
        bool isMotor;
        public bool IsMotor => isMotor;
        [SerializeField]
        bool isBrake;
        public bool IsBrake => isBrake;

        [SerializeField]
        bool isSteering;
        public bool IsSteering => isSteering;

        private void Awake()
        {
            wheels = GetComponentsInChildren<WheelCollider>();
        }

        private void ApplyLocalPositionToVisuals(WheelCollider collider)
        {
            var visualWheel = collider.transform.GetChild(0);
            collider.GetWorldPose(out var position, out var rotation);
            visualWheel.position = position;
            visualWheel.rotation = rotation;
        }

        private void FixedUpdate()
        {
            foreach (var wheel in wheels)
            {
                ApplyLocalPositionToVisuals(wheel);
            }
        }
        public bool SetMotorTorque(float motorTorque)
        {
            if (IsMotor)
            {
                foreach (var wheel in wheels)
                {
                    wheel.motorTorque = motorTorque;
                }
            }
            return IsMotor;
        }
        public bool SetBrakeTorque(float brakeTorque)
        {
            if (IsBrake)
            {
                foreach (var wheel in wheels)
                {
                    wheel.brakeTorque = brakeTorque;
                }
            }
            return IsBrake;
        }
        public bool SetSteerAngle(float steerAngle)
        {
            if (IsSteering)
            {
                foreach (var wheel in wheels)
                {
                    wheel.steerAngle = steerAngle;
                }
            }
            return IsSteering;
        }
    }
}
