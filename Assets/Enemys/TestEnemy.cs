using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    public class TestEnemy : MonoBehaviour
    {
        [SerializeField]
        private float healthPoint = 100;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                var f = collision.impulse;
                f.y += 1;
                GetComponent<Rigidbody>().AddForce(f,ForceMode.Impulse);
                healthPoint -= 100;

                if (healthPoint <= 0)
                {
                    Destroy(gameObject,1);
                }
            }
        }
    }
}
