﻿using System.Collections;
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
            scoreLabel.text = Singleton.Get<ScoreManager>().Score.ToString();
            Singleton.Get<ScoreManager>().OnChangeScore.Subscribe( UpdateScore);
        }

        private void UpdateScore(int score)
        {
            scoreLabel.text = score.ToString();
        }

    }
}
