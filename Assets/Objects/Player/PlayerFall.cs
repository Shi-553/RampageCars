using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    public class PlayerFall : MonoBehaviour
    {
        [SerializeField]
        private Transform self;
        
        
        void Start()
        {
            GetComponent<ISubscribeable<FallSeaInfo>>().Subscribe(Respawn);
        }


        void Respawn(FallSeaInfo _)
        {
            self.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            var nearObject = FetchNearObjectWithTag("Spawn");
            self.LookAt(nearObject);
            transform.parent.position = nearObject.position;

        }

        // １番近いオブジェクトを取得する
        private Transform FetchNearObjectWithTag(string tagName)
        {
            // 該当タグが1つしか無い場合はそれを返す
            var targets = GameObject.FindGameObjectsWithTag(tagName);
            if (targets.Length == 1) return targets[0].transform;

            GameObject result = null;
            var minTargetDistance = float.MaxValue;
            foreach (var target in targets)
            {
                // 前回計測したオブジェクトよりも近くにあれば記録
                var targetDistance = Vector3.SqrMagnitude(transform.position - target.transform.position);
                if (!(targetDistance < minTargetDistance)) continue;
                minTargetDistance = targetDistance;
                result = target.transform.gameObject;
            }

            // 最後に記録されたオブジェクトを返す
            return result?.transform;
        }
    }
}
