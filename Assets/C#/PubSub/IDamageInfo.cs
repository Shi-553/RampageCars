using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    public interface IDamageInfo : IInfo
    {
        public float Value { get; }
    }

    [System.Serializable]
    public class DamageInfo : IDamageInfo
    {
        [SerializeField]
        float value;
        public float Value
        {
            get => value;
            set => this.value = value;
        }
    }
}
