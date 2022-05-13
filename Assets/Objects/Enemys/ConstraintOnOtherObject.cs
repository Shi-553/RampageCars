using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    struct ColliderInfo
    {
        public GameObject obj;
        public int originalLayer;

        public ColliderInfo(GameObject obj, int layer)
        {
            this.obj = obj;
            this.originalLayer = layer;
        }
    }
    public class ConstraintOnOtherObject : MonoBehaviour
    {
        public bool IsConstant { get; private set; }
        Rigidbody rigid;

        Transform root;

        Transform other;
        Vector3 beforePos;

        List<ColliderInfo> colliderInfos;

        public void Constraint(Transform other)
        {
            IsConstant = true;
            rigid.isKinematic = true;

            List<Collider> colliders = new();
            GetComponentsInChildren(colliders);
            colliderInfos = colliders.Select(c => new ColliderInfo(c.gameObject, c.gameObject.layer)).ToList();

            foreach (var info in colliderInfos)
            {
                info.obj.layer = LayerMask.NameToLayer("Disable");
            }
            this.other = other;
            beforePos = other.InverseTransformPoint(transform.position);
        }

        public void UnConstraint()
        {
            IsConstant = false;
            rigid.isKinematic = false;

            foreach (var info in colliderInfos)
            {
                info.obj.layer = info.originalLayer;
            }
            colliderInfos.Clear();
            other = null;
        }


        void Start()
        {
            rigid = GetComponentInParent<Rigidbody>();
            root = rigid.transform;
        }

        void LateUpdate()
        {
            if (other == null)
                return;
            root.position = other.TransformPoint(beforePos);
        }
    }
}
