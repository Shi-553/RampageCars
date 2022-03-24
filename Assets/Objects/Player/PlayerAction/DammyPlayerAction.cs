using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    public class DammyPlayerAction : IPlayerAction
    {
        public bool IsPlaying => false;

        public void Do() {}
        public void Finish() { }
        public void OnCollisionEnter(Collision collision) { }
    }
}
