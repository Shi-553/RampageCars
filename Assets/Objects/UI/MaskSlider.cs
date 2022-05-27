using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    public class MaskSlider : MonoBehaviour
    {

        [SerializeField,Range(0,1)]
        float value;
        public float Value { get
            {
                return value;
            }
            set
            {
                this.value = Mathf.Clamp01(value);
                UpdateSlider();
            }
        }
        RectTransform rectTransform;
        Vector3 originalPos;
        Vector3 originalChildPos;

        void Start()
        {
            rectTransform=GetComponent<RectTransform>();
            originalPos=rectTransform.localPosition;
            originalChildPos = rectTransform.GetChild(0).position;
        }

        void UpdateSlider()
        {
            var pos = originalPos;
            pos.x -= rectTransform.sizeDelta.x;
            

            rectTransform.localPosition = Vector3.Lerp(originalPos, -pos, value);

            rectTransform.GetChild(0).position = originalChildPos;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if(rectTransform != null)
            UpdateSlider();
        }
#endif
    }
}
