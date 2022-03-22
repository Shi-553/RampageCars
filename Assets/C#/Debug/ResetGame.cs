using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;


namespace RampageCars
{
    public class ResetGame : MonoBehaviour
    {
        void Start()
        {

        }

        void Update()
        {
            var keyboard = Keyboard.current;
            var gamepad = Gamepad.current;

            if (keyboard.pKey.wasPressedThisFrame || (gamepad?.startButton.wasPressedThisFrame ?? false))
            {
                var loader = Singleton.Get<SceneLoader>();

                loader.ChangeScene(loader.CurrentType);
            }
        }
    }
}
