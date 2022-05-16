using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    public class OverreactionAtCollision : MonoBehaviour
    {
        [SerializeField]
        float scale = 1.0f;

        Rigidbody rigid;
        void Start()
        {
            GetComponent<ISubscribeable<CollisionDamageInfo>>().Subscribe(Collision);
            rigid = GetComponentInParent<Rigidbody>();
        }

        // Update is called once per frame
        void Collision(CollisionDamageInfo info)
        {
            rigid.AddForce(info.fixedImpulse * scale);
        }
    }
}
