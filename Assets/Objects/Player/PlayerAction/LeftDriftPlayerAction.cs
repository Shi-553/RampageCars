using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EditorScripts;

namespace RampageCars
{
    public class LeftDriftPlayerAction : DriftPlayerActionBase
    {
        [SerializeField]
        float rotatePower = 5;
        public override float RotatePower => -rotatePower;
        public override string MotionName => "IsDriftL";
    }
}
