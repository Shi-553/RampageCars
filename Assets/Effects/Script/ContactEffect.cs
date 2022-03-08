using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    public class ContactEffect : MonoBehaviour
    {
        public GameObject hit, explosion;
        public float fuseTime;

        private void Explode()
        {
            //パーティクル用ゲームオブジェクト生成
            Instantiate(explosion, this.transform.position, Quaternion.identity); 
        }

        private void OnCollisionEnter(Collision collision)
        {
            //Playerタグの付いたゲームオブジェクトと衝突したか判別
            if (collision.gameObject.tag == "Player") 
            {
                Instantiate(hit, this.transform.position, Quaternion.identity);

                // 時間経過で出現
                Invoke("Explode", fuseTime);
            }
        }
    }
}
