using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    public interface IPlayerAction
    {
        void Do();
        void Finish();

        bool IsPlaying { get; }
        void OnCollisionEnter(Collision collision);
    }
}
