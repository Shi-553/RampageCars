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
            action.Player.Accel.canceled += AccelCanceled;
            action.Player.Jump.started += JumpStarted;
            action.Player.Wiper.started += WiperStarted;
            action.Player.Wiper.canceled += WiperCanceled;

            action.Player.DriftL.started += DriftLStarted;
            action.Player.DriftR.started += DriftRStarted;
            action.Player.DriftL.canceled += DriftLCanceled;
            action.Player.DriftR.canceled += DriftRCanceled;

            player.SetMotorTorque(1);
        }

        private void AccelCanceled(InputAction.CallbackContext obj)
        {
            actionManager.FinishAction<AccelPlayerAction>();
        }

        private void DriftLStarted(InputAction.CallbackContext obj)
        {
            actionManager.DoAction<LeftDriftPlayerAction>();
        }
        private void DriftRStarted(InputAction.CallbackContext obj)
        {
            actionManager.DoAction<RightDriftPlayerAction>();
        }
        private void DriftLCanceled(InputAction.CallbackContext obj)
        {
            actionManager.FinishAction<LeftDriftPlayerAction>();
        }
        private void DriftRCanceled(InputAction.CallbackContext obj)
        {
            actionManager.FinishAction<RightDriftPlayerAction>();
        }
        private void WiperStarted(InputAction.CallbackContext obj)
        {
            actionManager.DoAction<WiperPlayerAction>();
        }
        private void WiperCanceled(InputAction.CallbackContext obj)
        {
            actionManager.FinishAction<WiperPlayerAction>();
        }

        private void JumpStarted(InputAction.CallbackContext obj)
        {
            actionManager.DoAction<DivePlayerAction>();
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
