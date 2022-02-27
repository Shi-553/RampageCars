using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    public class TestEnemy : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                var f = collision.impulse;
                f.y += 1;
                GetComponent<Rigidbody>().AddForce(f,ForceMode.Impulse);

                Destroy(gameObject,1);
            }
        }
    }
}
