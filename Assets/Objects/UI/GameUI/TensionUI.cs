using System.Collections;
using System.Collections.Generic;
using EditorScripts;
using UnityEngine;
using UnityEngine.UI;

namespace RampageCars
{
    public class TensionUI : MonoBehaviour
    {
        [field: Range(0, 1)]
        public float Value { get; private set; }

        [SerializeField]
        SerializeInterface<ISubscribeable<TensionPercentInfo>> onPercent;

        [SerializeField, NotNull]
        Slider tensionBar;
        [SerializeField]
        List<GameObject> tensionFaces;
        GameObject currnetTensionFace;

        void Start()
        {
            onPercent.Interface.Subscribe(OnPercent);
            OnPercent(new(0));
        }
        void OnPercent(TensionPercentInfo percentInfo)
        {
            tensionBar.value = percentInfo.Percent;

            var onePercent = 1.0f / tensionFaces.Count;
            
            var index = Mathf.FloorToInt(percentInfo.Percent / onePercent);
            index = Mathf.Clamp(index, 0,tensionFaces.Count-1);

            if (tensionFaces[index] == currnetTensionFace)
            {
                return;
            }
            if (currnetTensionFace != null)
                currnetTensionFace.SetActive(false);

            tensionFaces[index].SetActive(true);
            currnetTensionFace=tensionFaces[index];
        }
    }
}
