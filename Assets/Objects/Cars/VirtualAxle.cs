using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace RampageCars
{
    class VirtualAxle : MonoBehaviour, IAxle
    {
        List<Transform> wheelMeshs = new();

        [SerializeField]
        bool isMotor;
        public bool IsMotor => isMotor;
        [SerializeField]
        bool isBrake;
        public bool IsBrake => isBrake;

        [SerializeField]
        bool isSteering;
        public bool IsSteering => isSteering;

        float motorTorque;
        float brakeTorque;
        float steerAngle;

        SpeedMeasure speedMeasure;

        private void Awake()
        {
            wheelMeshs.Clear();
            wheelMeshs = transform.Cast<Transform>().Select(t => t.GetChild(1)).ToList();
            speedMeasure = GetComponentInParent<SpeedMeasure>();
        }


        public void SetMotorTorque(float motorTorque)
        {
            if (!IsMotor)
            {
                return;
            }
            this.motorTorque = motorTorque;
        }

        public void SetBrakeTorque(float brakeTorque)
        {
            if (!IsBrake)
            {
                return;
            }
            this.brakeTorque = brakeTorque;
        }

        public void SetSteerAngle(float steerAngle)
        {
            if (!IsSteering)
            {
                return;
            }
            this.steerAngle = steerAngle;
            var steer = Quaternion.AngleAxis(steerAngle, Vector3.right);
            foreach (var wheel in wheelMeshs)
            {
                wheel.localRotation = steer;
            }
        }
    }
}
