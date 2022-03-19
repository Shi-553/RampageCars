using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace RampageCars
{
    public class QuitGame : MonoBehaviour
    {

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            var keyboard = Keyboard.current;

            if (keyboard.lKey.wasPressedThisFrame)
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }

            var gamepad = Gamepad.current;

            if (gamepad == null)
            {
                return;
            }

            if (gamepad.selectButton.wasPressedThisFrame)
            {

#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }
        }
    }
}
