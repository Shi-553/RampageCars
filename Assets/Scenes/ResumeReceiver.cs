using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RampageCars
{
    public class ResumeReceiver : MonoBehaviour
    {
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(ChangeScene);
        }

        public void ChangeScene()
        {
            var loader = Singleton.Get<SceneLoader>();
            loader.Resume();
        }
    }
}
