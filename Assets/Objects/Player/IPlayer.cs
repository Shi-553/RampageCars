using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RampageCars
{
    interface IPlayer
    {
        void SetMotorTorque(float motorTorque);
        void SetSteerAngle(float steerAngle);
        void Jamp();
        void DoAction<T>() where T : IPlayerAction;
        void FinishAction<T>() where T : IPlayerAction;
    }
}
