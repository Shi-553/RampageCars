using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;


namespace RampageCars
{
    public class TestEnemySponer : MonoBehaviour
    {
        [SerializeField]
        AssetReferenceGameObject testEnemyRef;
        GameObject testEnemy;

        float time;
        [SerializeField]
        float sponeSpeed=1;
        async Task Start()
        {
            testEnemy=await testEnemyRef.LoadAssetAsync<GameObject>().Task;

        }
        private void OnDestroy()
        {
            testEnemyRef.ReleaseAsset();
        }
        // Update is called once per frame
        void Update()
        {
            time += Time.deltaTime* sponeSpeed;

            if (time > 1)
            {
                time = 0;
                var obj=Instantiate(testEnemy);
                var pos =new Vector3(Random.Range(-10,10),5, Random.Range(-10, 10));
                obj.transform.position= pos;
                obj.transform.parent = transform;
            }
        }
    }
}
