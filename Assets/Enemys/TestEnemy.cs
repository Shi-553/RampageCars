using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RampageCars
{

    public class TestEnemy : MonoBehaviour, IEnemyTag, IAffectable<ICollisionDamageInfo>, IDeathable
    {
        [SerializeField]
        private float healthPoint = 100;

        ActionWrapper<ICollisionDamageInfo> onAffect = new();
        public ISubscribeableAction<ICollisionDamageInfo> OnAffect => onAffect;

        ActionWrapper onDeath = new();
        public ISubscribeableAction OnDeath => onDeath;

        [SerializeField]
        float destroyDelayTime = 1;
        public bool IsDeath => healthPoint <= 0;



        public void Affect(ICollisionDamageInfo info)
        {
            if (IsDeath)
            {
                return;
            }

            var f = info.Collision.impulse;
            f.y += 1;

            GetComponent<Rigidbody>().AddForce(f, ForceMode.Impulse);

            onAffect?.Invoke(info);
            healthPoint -= info.Value;

            if (IsDeath)
            {
                StartCoroutine(DelayDestroy());
            }
        }

        IEnumerator DelayDestroy()
        {
            yield return new WaitForSeconds(destroyDelayTime);

            onDeath?.Invoke();
            Destroy(gameObject);
        }

    }
}
