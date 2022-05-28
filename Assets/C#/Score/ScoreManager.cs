using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RampageCars
{
    public class ScoreManager : SingletonBase
    {
        public int Score { get; private set; } = 0;

        public ActionWrapper<int> OnChangeScore=new();
        void Start()
        {
        }

        public void AddScore(int amount)
        {
            Score += amount;
            OnChangeScore.Publish(Score);
        }
    }
}
