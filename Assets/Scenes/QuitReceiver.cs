using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RampageCars
{
    [RequireComponent(typeof(Button))]
    public class QuitReceiver : MonoBehaviour
    {
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(Quit);
        }
        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
