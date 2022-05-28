using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EditorScripts;

namespace RampageCars
{
    public class HPUI : MonoBehaviour
    {
        [SerializeField]
        SerializeInterface<ISubscribeable<HPPercentInfo>> onPercent;

        [SerializeField,NotNull]
        Slider hpBar;
        void Start()
        {
            onPercent.Interface.Subscribe(OnPercent);
        }
        void OnPercent(HPPercentInfo percentInfo) {
            hpBar.value = percentInfo.Percent;
        }
    }
}
