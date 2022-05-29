using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RampageCars
{
    [RequireComponent(typeof(Button))]
    public class ChangeSceneReceiver : MonoBehaviour
    {
        [SerializeField]
        SceneType nextScene;
        [SerializeField]
        bool isOverride = false;

        void Start()
        {
            GetComponent<Button>().onClick.AddListener(ChangeScene);
        }

        public void ChangeScene()
        {
            var loader = Singleton.Get<SceneLoader>();
            if (!isOverride)
            {
                loader.ChangeScene(nextScene);
            }
            else
            {
                loader.OverrideScene(nextScene);
            }
        }
    }
}
