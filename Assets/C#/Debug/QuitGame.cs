using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace RampageCars
{
    public class QuitGame : MonoBehaviour
    {

        void Start()
        {

        }

        void Update()
        {
            var keyboard = Keyboard.current;
            var gamepad = Gamepad.current;

            if (keyboard.lKey.wasPressedThisFrame || (gamepad?.selectButton.wasPressedThisFrame ?? false))
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }
        }

        public void OnClickStartButton()
        {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
