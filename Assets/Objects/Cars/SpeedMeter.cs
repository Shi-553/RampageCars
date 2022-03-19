using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RampageCars
{
    public class SpeedMeter : MonoBehaviour
    {
        [SerializeField]
        SpeedMeasure speedMeasure;
        TMP_Text speed;
        void Start()
        {
            var texts = GetComponentsInChildren<TMP_Text>();
            speed = texts[0];
        }

        // Update is called once per frame
        void Update()
        {
            speed.text = "speed\n" + speedMeasure.Speed.ToString("F");
        }
    }
}
