using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RampageCars
{
    public class Timer : MonoBehaviour
    {
        [SerializeField]
        float TimeLimit = 180;

        TMP_Text time;

        void Start()
        {
            var texts = GetComponentsInChildren<TMP_Text>();
            time = texts[0];
        }

        void Update()
        {
            TimeLimit -= Time.deltaTime;

            // 残り秒数が0になったとき用
            if (TimeLimit <= 0.0)
            {
                TimeLimit = 0.0f;
            }

            time.text = TimeLimit.ToString("F2");
        }

    }
}
