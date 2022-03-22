using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampageCars
{
    interface IAxle
    {
        public bool IsMotor { get; }
        public bool IsBrake { get; }
        public bool IsSteering { get; }
        public void SetMotorTorque(float motorTorque);
        public void SetBrakeTorque(float brakeTorque);
        public void SetSteerAngle(float steerAngle);
    }
}
