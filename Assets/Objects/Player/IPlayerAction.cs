using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    public interface IPlayerAction
    {
        void Do();
        void Finish();

        bool CanChange { get; }
        void CollisionEnter(Collision collision) { }
        void CollisionStay(Collision collision) { }
        void CollisionExit(Collision collision) { }
    }
}
