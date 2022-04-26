using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

namespace RampageCars
{
    public class Timer : MonoBehaviour
    {
        [SerializeField]
        float timeLimit = 4000;

        TMP_Text time;
        bool isEnableTimer = false;

        public event UnityAction OnTimerEnd;

        void Start()
        {
            time = GetComponentInChildren<TMP_Text>();
            isEnableTimer = true;
        }

        void Update()
        {
            if (!isEnableTimer)
            {
                return;
            }
            timeLimit -= Time.deltaTime;

            // 残り秒数が0になったとき用
            if (timeLimit <= 0.0)
            {
                timeLimit = 0.0f;
                OnTimerEnd?.Invoke();
                isEnableTimer = false;
            }

            time.text = Mathf.FloorToInt(timeLimit).ToString();
        }

    }
}
