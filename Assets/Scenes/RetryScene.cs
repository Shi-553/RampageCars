using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RampageCars
{
    public class RetryScene : MonoBehaviour
    {
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(Retry);
        }
        void Retry()
        {
            var loader=Singleton.Get<SceneLoader>();
            loader.ChangeScene(loader.CurrentType);
        }

    }
}
