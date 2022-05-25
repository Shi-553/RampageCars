using System.Collections;
using System.Collections.Generic;
using EditorScripts;
using UnityEngine;

namespace RampageCars
{
    public class ScoreAdder : MonoEventReceiver
    {
        [SerializeField]
        public int scoreValue;

        private void Start()
        {
            OnDestroyed += GetComponent<ISubscribeable<DelayDestroyInfo>>().Subscribe(OnDeath);
        }

        private void OnDeath(DelayDestroyInfo n)
        {
            Singleton.Get<ScoreManager>().AddScore(scoreValue);
        }
    }
}
