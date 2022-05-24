using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RampageCars
{
    public class ScoreDrawer : MonoBehaviour
    {
        private TMP_Text scoreLabel;

        void Start()
        {
            scoreLabel = GetComponent<TMP_Text>();
            scoreLabel.text = "SCORE:" + Singleton.Get<ScoreManager>().Score;
        }

        private void Update()
        {
            scoreLabel.text = "SCORE:" + Singleton.Get<ScoreManager>().Score;
        }

    }
}
