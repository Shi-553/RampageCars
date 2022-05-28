using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampageCars
{
    public readonly struct HPPercentInfo
    {
        public readonly float Percent;
        public HPPercentInfo(float p)
        {
            Percent = Mathf.Clamp01(p);
        }

    }
}
