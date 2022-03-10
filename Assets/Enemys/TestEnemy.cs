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

        public ActionWrapper<ICollisionDamageInfo> OnAffect { get; private set; } = new();
        public ActionWrapper OnDeath { get; private set; } = new();

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

            healthPoint -= info.Value;
            OnAffect?.Invoke(info);

            if (IsDeath)
            {
                StartCoroutine(DelayDestroy());
            }
        }

        IEnumerator DelayDestroy()
        {
            yield return new WaitForSeconds(destroyDelayTime);

            Destroy(gameObject);
            OnDeath?.Invoke();
        }

    }
}
