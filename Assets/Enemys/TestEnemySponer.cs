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
                var pos = new Vector3(Random.Range(-30, 30), 5, Random.Range(-30, 30));
                obj.transform.position = pos;
                obj.transform.parent = transform;
            }
        }
    }
}
