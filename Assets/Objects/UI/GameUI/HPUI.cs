using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EditorScripts;

namespace RampageCars
{
    public class HPUI : MonoBehaviour
    {

        [SerializeField,NotNull]
        Slider hpBar;

        void Start()
        {
            Singleton.Get<PlayerSingleton>()
                .GetComponentInChildren<ISubscribeable<HPPercentInfo>>()
                .Subscribe(OnPercent);
        }
        void OnPercent(HPPercentInfo percentInfo) {
            hpBar.value = percentInfo.Percent;
        }
    }
}
