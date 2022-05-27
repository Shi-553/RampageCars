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
            var publishable = collision.gameObject.GetComponent<IPublishable<FallSeaInfo>>();
            if (publishable is IPlayerTag)
            {
                publishable.Publish(new (playerDamage));
            }
            if (publishable is IEnemyTag)
            {
                publishable.Publish(new (enemyDamage));
            }
        }
    }
}
