using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    public class ShotEnemyTest : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("対象物(向く方向)")]
        private GameObject target;
        [SerializeField]
        private GameObject bullet;
        [SerializeField]
        private float bulletSpeed=50;
        [SerializeField]
        private float shotSpeed = 100;

        private float shottime=0;
        private void Update()
        {
            shottime++;
            // 対象物と自分自身の座標からベクトルを算出
            Vector3 vector3 = target.transform.position - this.transform.position;
            // もし上下方向の回転はしないようにしたければ以下のようにする。
             vector3.y = 0f;

            // Quaternion(回転値)を取得
            Quaternion quaternion = Quaternion.LookRotation(vector3);
            // 算出した回転値をこのゲームオブジェクトのrotationに代入
            this.transform.rotation = quaternion;

            if (shottime > shotSpeed)
            {
                GameObject runcherBullet = GameObject.Instantiate(bullet) as GameObject; //runcherbulletにbulletのインスタンスを格納
                runcherBullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed; //アタッチしているオブジェクトの前方にbullet speedの速さで発射
                runcherBullet.transform.position = transform.position;
                shottime = 0;
            }
        }
    }
}
