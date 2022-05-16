using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampageCars
{
    public readonly struct DamageInfo
    {
        public readonly float damage;

        public DamageInfo(float damage) => this.damage = damage;
    }
}
