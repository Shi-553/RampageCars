using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    public class PlayerFall : MonoBehaviour
    {
        [SerializeField]
        GroundChecker groundChecker;
        [SerializeField]
        private RespawnPoint respawnPoint;

        void Start()
        {
            GetComponentInChildren<ISubscribeable<FallSeaInfo>>().Subscribe(Respawn);
        }


        void Respawn(FallSeaInfo _)
        {
            GetComponentInParent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            if (respawnPoint == null)
            {
                Debug.LogError("リスポーンポイントが設定されていません。");
                return;
            }
            transform.rotation = respawnPoint.Rotation;
            transform.position = respawnPoint.Position;
        }

        void OnTriggerStay(Collider other)
        {
            if (groundChecker.IsGrounded)
            {
                if (other.CompareTag("Spawn"))
                {
                    respawnPoint = other.GetComponent<RespawnPoint>();
                }
            }
        }
    }
}
