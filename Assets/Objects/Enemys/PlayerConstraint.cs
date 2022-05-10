using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    public class PlayerConstraint : MonoBehaviour
    {
        [SerializeField]
        Rigidbody rigid;
        
        [SerializeField]
        Transform root;

        int originalLayer;

        Transform player;
        Vector3 beforePos;

        public void Constraint (Transform player)
        {
            rigid.isKinematic = true;
            originalLayer = gameObject.layer;
            gameObject.layer = LayerMask.NameToLayer("Disable");
            this.player = player;
            beforePos = player.position;
        }

        public void UnConstraint ()
        {
            rigid.isKinematic = false;
            gameObject.layer = originalLayer;
            player = null;

        }


        void Start()
        {
        
        }

        void LateUpdate()
        {
            if (player==null)
                return;
            root.position += player.position- beforePos;
            beforePos = player.position;
        }
    }
}
