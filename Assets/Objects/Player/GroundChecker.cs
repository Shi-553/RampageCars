using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    public class GroundChecker : MonoBehaviour
    {
        int groundCount = 0;

        public bool IsGrounded => groundCount != 0;

        void Start()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            groundCount++;
        }
        private void OnTriggerExit(Collider other)
        {
            groundCount--;
        }
    }
}
