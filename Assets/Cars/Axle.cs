using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    public class Axle : MonoBehaviour
    {
        [SerializeField]
        WheelCollider leftWheel;
        [SerializeField]
        WheelCollider rightWheel;

        [SerializeField]
        bool isMotor;
        public bool IsMotor => isMotor;
        [SerializeField]
        bool isBrake;
        public bool IsBrake => isBrake;

        [SerializeField]
        bool isSteering;
        public bool IsSteering => isSteering;

        private void ApplyLocalPositionToVisuals(WheelCollider collider)
        {
            var visualWheel = collider.transform.GetChild(0);
            collider.GetWorldPose(out var position, out var rotation);
            visualWheel.position = position;
            visualWheel.rotation = rotation;
        }

        private void FixedUpdate()
        {
            ApplyLocalPositionToVisuals(leftWheel);
            ApplyLocalPositionToVisuals(rightWheel);
        }
        public bool SetMotorTorque(float motorTorque)
        {
            if (IsMotor)
            {
                leftWheel.motorTorque = motorTorque;
                rightWheel.motorTorque = motorTorque;
            }
            return IsMotor;
        }
        public bool SetBrakeTorque(float brakeTorque)
        {
            if (IsBrake)
            {
                leftWheel.brakeTorque = brakeTorque;
                rightWheel.brakeTorque = brakeTorque;
            }
            return IsBrake;
        }
        public bool SetSteerAngle(float steerAngle)
        {
            if (IsSteering)
            {
                leftWheel.steerAngle = steerAngle;
                rightWheel.steerAngle = steerAngle;
            }
            return IsSteering;
        }
    }
}
