using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    public class PlayerDamageEffect : MonoBehaviour
    {
        [SerializeField]
        GameObject effect;

        [SerializeField]
        Transform effectParent;

        [SerializeField]
        Material damageMat;
        [SerializeField]
        Material defaultMat;

        [SerializeField]
        SkinnedMeshRenderer damageSkinnedMeshRenderer;

        void Start()
        {
            defaultMat =damageSkinnedMeshRenderer.sharedMaterial;
            GetComponent<ISubscribeable<DamageInfo>>().Subscribe(info=>StartCoroutine(OnDamage(info)));
        }
        IEnumerator OnDamage(DamageInfo info)
        {
            Instantiate(effect, effectParent);
            damageSkinnedMeshRenderer.sharedMaterial = damageMat;
            yield return new WaitForSeconds(0.7f);

            damageSkinnedMeshRenderer.sharedMaterial = defaultMat;
        }

    }
}
