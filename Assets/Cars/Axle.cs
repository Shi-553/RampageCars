using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    public class Axle : MonoBehaviour, IAxle
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
        public void SetMotorTorque(float motorTorque)
        {
            if (!IsMotor)
            {
                return ;
            }

            foreach (var wheel in wheels)
            {
                wheel.motorTorque = motorTorque;
            }
        }
        public void SetBrakeTorque(float brakeTorque)
        {
            if (!IsBrake)
            {
                return ;
            }

            foreach (var wheel in wheels)
            {
                wheel.brakeTorque = brakeTorque;
            }
        }
        public void SetSteerAngle(float steerAngle)
        {
            if (!IsSteering)
            {
                return ;
            }

            foreach (var wheel in wheels)
            {
                wheel.steerAngle = steerAngle;
            }

        }
    }
}
