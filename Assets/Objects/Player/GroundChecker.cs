using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace RampageCars
{
    public class GroundChecker : MonoBehaviour
    {
        int groundCount = 0;

        public bool IsGrounded => groundCount != 0;

        string groundTag = "Ground";

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == groundTag)
            {
                groundCount++;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.tag == groundTag)
            {
                groundCount--;
            }
        }
    }
}
