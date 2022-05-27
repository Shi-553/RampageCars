using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System;

namespace RampageCars
{

    public class Timer : MonoBehaviour
    {
        [SerializeField]
        float timeLimit = 180;

        TMP_Text time;
        bool isEnableTimer = false;


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
                Singleton.Get<GameRoleManager>().GameOver();

                isEnableTimer = false;
            }

            var span = new TimeSpan(0, 0, Mathf.FloorToInt(timeLimit));
            var hhmmss = span.ToString(@"mm\:ss");
            time.text = hhmmss;
        }

    }
}
