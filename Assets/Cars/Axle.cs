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
        bool isSteering;
        public bool IsSteering => isSteering;

        public bool SetMotorTorque(float motorTorque)
        {
            if (IsMotor)
            {
                leftWheel.motorTorque = motorTorque;
                rightWheel.motorTorque = motorTorque;
            }
            return IsMotor;
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
