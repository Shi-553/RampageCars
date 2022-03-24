using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RampageCars
{
    [RequireComponent(typeof(SpeedMeasure))]
    public class PlayerController : MonoBehaviour
    {
        PlayerAction action;
        PlayerMover player;
        PlayerActionManager actionManager;

        private void OnEnable()
        {
            action = new();
            action.Enable();
        }

        private void OnDisable()
        {
            action.Disable();
        }

        void Start()
        {
            actionManager = GetComponent<PlayerActionManager>();
            player = GetComponent<PlayerMover>();
            action.Player.Accel.performed += BoostPerformed;
            action.Player.Jump.started += JumpStarted;

            player.SetMotorTorque(1);
        }

        private void JumpStarted(InputAction.CallbackContext obj)
        {
            player.Jamp();
        }


        private void BoostPerformed(InputAction.CallbackContext obj)
        {
            actionManager.DoAction<AccelPlayerAction>();
        }


        void Update()
        {
            var steer = action.Player.Steer.ReadValue<float>();
            player.SetSteerAngle(steer);

            var motor = action.Player.Motor.ReadValue<float>();
            if (motor < 0)
            {
                player.SetMotorTorque(motor);
            }
            else
            {
                player.SetMotorTorque(1);
            }
        }
    }
}
