using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RampageCars
{
    public class ScoreManager : SingletonBase
    {
        public int Score { get; private set; } = 0;

        void Start()
        {
        }

        public void AddScore(int amount)
        {
            Score += amount;
        }
    }
}
