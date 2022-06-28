using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    public class RespawnPoint : MonoBehaviour
    {
        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawCube(GetComponent<BoxCollider>().center, GetComponent<BoxCollider>().size);

        }
#endif
    }
}
