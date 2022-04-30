using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    public class PlayerFall : MonoBehaviour
    {
        public static PlayerFall instance;
        
        [SerializeField]
        private Transform self;
        
        [SerializeField]
        private Transform target;

        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void Respawn()
        {
            self.LookAt(target);
            this.transform.position = FetchNearObjectWithTag("Spawn").position;
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
                var targetDistance = Vector3.Distance(transform.position, target.transform.position);
                if (!(targetDistance < minTargetDistance)) continue;
                minTargetDistance = targetDistance;
                result = target.transform.gameObject;
            }

            // 最後に記録されたオブジェクトを返す
            return result?.transform;
        }
    }
}
