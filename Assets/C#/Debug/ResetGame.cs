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
                SceneManager.LoadScene("SampleScene");
            }

            var gamepad = Gamepad.current;

            if (gamepad == null)
            {
                return;
            }

            if (gamepad.startButton.wasPressedThisFrame)
            {

                SceneManager.LoadScene("SampleScene");
            }
        }


    }
}
