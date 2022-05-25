using System;
using System.Collections;
using UnityEngine;

namespace RampageCars
{
    public class DelayDestroy : MonoBehaviour, ISubscribeableImpl<DelayDestroyInfo>
    {
        public ActionWrapper<DelayDestroyInfo> PubSubAction { get; } = new();

        [SerializeField]
        float destroyDelayTime = 1;

        private void Start()
        {
            GetComponent<ISubscribeable<DeathInfo>>().Subscribe((_) => StartCoroutine(Delay()));
        }

        IEnumerator Delay()
        {
            yield return new WaitForSeconds(destroyDelayTime);

            Destroy(transform.parent.gameObject);
            PubSubAction.Publish(new());
        }
    }
}
