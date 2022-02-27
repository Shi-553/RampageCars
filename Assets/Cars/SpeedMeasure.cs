using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    class SpeedMeasure: MonoBehaviour
    {

        Rigidbody rigid;
        public float Speed { private set; get; }
        public float SpeedForward { private set; get; }

        private void Awake()
        {
            rigid = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            Speed = rigid.velocity.magnitude;
            SpeedForward = Vector3.Dot(rigid.velocity, transform.forward);
        }
    }
}
