using UnityEngine;

namespace RampageCars
{
    public readonly struct TensionPercentInfo
    {
        public readonly float Percent;
        public TensionPercentInfo(float p)
        {
            Percent = Mathf.Clamp01(p);
        }

    }
}
