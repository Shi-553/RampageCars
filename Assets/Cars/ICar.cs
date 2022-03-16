using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RampageCars
{
    interface ICar
    {
        void SetMotorTorque(float motorTorque);
        void SetBrakeTorque(float brakeTorque);
        void SetSteerAngle(float steerAngle);
        void Boost(Vector3 v);
        void Jamp();
        void GameReset();
    }
}
