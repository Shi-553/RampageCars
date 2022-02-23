using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    public class Car : MonoBehaviour
    {
        [SerializeField]
        List<Axle> axles;


        public void SetMotorTorque(float motorTorque)
        {
            foreach (var axle in axles)
            {
                axle.SetMotorTorque(motorTorque);
            }
        }
        public void SetSteerAngle(float steerAngle)
        {
            foreach (var axle in axles)
            {
                axle.SetSteerAngle(steerAngle);
            }
        }
    }
}