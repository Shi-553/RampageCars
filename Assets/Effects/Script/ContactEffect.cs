using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    public class ContactEffect : MonoBehaviour
    {
        public GameObject particleObject;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Player") //Playerタグの付いたゲームオブジェクトと衝突したか判別
            {
                Instantiate(particleObject, this.transform.position, Quaternion.identity); //パーティクル用ゲームオブジェクト生成
            }
        }
    }
}
