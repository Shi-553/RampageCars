using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using EditorScripts;
using UnityEngine;
using UnityEngine.AddressableAssets;


namespace RampageCars
{
    public class TestEnemySponer : MonoBehaviour
    {
        [SerializeField, NotNull]
        GameObject testEnemy;

        float time;
        [SerializeField]
        float sponeSpeed = 1;
        [SerializeField]
        float sponeMax = 100;
        [SerializeField]
        Vector3 sponeRange = new(10, 5, 10);

        // Update is called once per frame
        void Update()
        {
            time += Time.deltaTime * sponeSpeed;

            if (time > 1)
            {
                time = 0;
                if (transform.childCount > sponeMax)
                {
                    return;
                }
                var obj = Instantiate(testEnemy);

                var pos = new Vector3(
                    Random.Range(-sponeRange.x, sponeRange.x),
                    Random.Range(-sponeRange.y, sponeRange.y),
                    Random.Range(-sponeRange.z, sponeRange.z)
                    );
                obj.transform.parent = transform;
                obj.transform.localPosition = pos ;
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawCube(Vector3.zero, sponeRange*2);
        }
#endif
    }
}
