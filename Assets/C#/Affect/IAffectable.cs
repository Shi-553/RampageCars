using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RampageCars
{
    public interface IAffectInfo
    {
    }

    public interface IAffectable<Info> where Info : IAffectInfo
    {
        ActionWrapper<Info> OnAffect { get; }
        void Affect(Info info);
    }
}
