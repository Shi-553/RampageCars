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

            if (keyboard.pKey.wasPressedThisFrame)
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                SceneManager.LoadScene("SampleScene");
#endif
            }

            var gamepad = Gamepad.current;

            if (gamepad == null)
            {
                return;
            }

            if (gamepad.startButton.wasPressedThisFrame)
            {

#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                SceneManager.LoadScene("SampleScene");
#endif
            }
        }


    }
}
