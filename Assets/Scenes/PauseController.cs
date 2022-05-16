using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RampageCars
{
    public class PauseController : MonoBehaviour
    {
        PlayerAction action;

        private void OnEnable()
        {
            action = new();
            action.Enable();
        }
        void Start()
        {
            action.UI.Pause.started += PauseStarted;
        }

        private void PauseStarted(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            var loader = Singleton.Get<SceneLoader>();
            if (loader.IsPause)
            {
                loader.Resume();
            }
            else
            {
                loader.Pause();
            }
        }
    }
}
