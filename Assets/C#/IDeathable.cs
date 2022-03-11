using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RampageCars
{
    public interface IDeathable
    {
        public ISubscribeableAction OnDeath { get; }
    }
}
