using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    public class Sea : MonoBehaviour
    {
        [SerializeField]
        float enemyDamage = 9999;
        [SerializeField]
        float playerDamage = 5;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnTriggerEnter(Collider collision)
        {
            var publishable = collision.gameObject.GetComponent<IPublishable<CollisionDamageInfo>>();
            if (publishable is not null and IPlayerTag)
            {
                var impulse = new Vector3(0, 0, 0);
                publishable.Publish(new (playerDamage,impulse));
                collision.gameObject.GetComponent<PlayerFall>().Respawn();
            }
            if (publishable is not null and IEnemyTag)
            {
                var impulse = new Vector3(0, 0, 0);
                publishable.Publish(new (enemyDamage, impulse));
            }
        }
    }
}
