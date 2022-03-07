using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    public class SelfDestroy : MonoBehaviour
    {
        ParticleSystem Particle;
        void Start()
        {
            Particle = this.GetComponent<ParticleSystem>();
        }

        void Update()
        {
            if (Particle.isStopped) //パーティクルが終了したか判別
            {
                Destroy(this.gameObject);//パーティクル用ゲームオブジェクトを削除
            }
        }
    }
}
