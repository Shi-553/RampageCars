using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    public class TensionManager : MonoBehaviour, ISubscribeableImpl<TensionPercentInfo>
    {
        public ActionWrapper<TensionPercentInfo> PubSubAction { get; } = new();
        [SerializeField]
        float maxSpeed = 100;

        SpeedMeasure measure;
        void Start()
        {
            measure = GetComponent<SpeedMeasure>();
        }

        private void Update()
        {
            PubSubAction.Publish(new(measure.Speed/ maxSpeed));
        }
    }
}
