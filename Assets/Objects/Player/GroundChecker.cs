using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace RampageCars
{
    public readonly struct GroundInfo
    {

        public readonly bool isGrounded;

        public GroundInfo(bool isGrounded) => this.isGrounded = isGrounded;
    };
    public class GroundChecker : MonoBehaviour,ISubscribeableImpl<GroundInfo>
    {
        int groundCount = 0;

        public bool IsGrounded => groundCount != 0;

        public ActionWrapper<GroundInfo> PubSubAction { get; } = new();

        string groundTag = "Ground";

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == groundTag)
            {
                if (!IsGrounded)
                    PubSubAction.Publish(new(true));
                groundCount++;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.tag == groundTag)
            {
                if (IsGrounded)
                    PubSubAction.Publish(new(false));
                groundCount--;
            }
        }
    }
}
