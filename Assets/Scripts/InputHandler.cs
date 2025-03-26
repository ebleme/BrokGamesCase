// maebleme2

using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Ebleme
{
    // Todo: Zenject e dahil et
    public class InputHandler : MonoBehaviour
    {
        private PlayerInputs inputs;

        public Vector2 MoveInput { get; private set; }
        public Vector2 LookInput { get; private set; }
        public bool InteractPressed { get; private set; }
        public bool JumpPressed { get; set; }
        public bool JumpPressedDown { get; private set; }
        public bool SprintPressed { get; private set; }

        public event Action OnJumpPressed;


        private void Awake()
        {
            inputs = new PlayerInputs();

            inputs.Player.Move.performed += OnMovePerformed;
            inputs.Player.Move.canceled += OnMovePerformed;

            inputs.Player.Look.performed += OnLookPerformed;
            inputs.Player.Look.canceled += OnLookPerformed;

            inputs.Player.Interact.started += OnInteractStarted;
            inputs.Player.Interact.canceled += OnInteractCanceled;

            inputs.Player.Jump.started += OnJumpStarted;
            inputs.Player.Jump.canceled += OnJumpCanceled;

            inputs.Player.Sprint.started += OnSprintStarted;
            inputs.Player.Sprint.canceled += OnSprintCanceled;
        }

        private void OnDestroy()
        {
            inputs.Player.Move.performed -= OnMovePerformed;
            inputs.Player.Move.canceled -= OnMovePerformed;

            inputs.Player.Look.performed -= OnLookPerformed;
            inputs.Player.Look.canceled -= OnLookPerformed;

            inputs.Player.Interact.started -= OnInteractStarted;
            inputs.Player.Interact.canceled -= OnInteractCanceled;

            inputs.Player.Jump.started -= OnJumpStarted;
            inputs.Player.Jump.canceled -= OnJumpCanceled;

            inputs.Player.Sprint.started -= OnSprintStarted;
            inputs.Player.Sprint.canceled -= OnSprintCanceled;
        }

        private void OnSprintCanceled(InputAction.CallbackContext ctx)
        {
            SprintPressed = false;
        }

        private void OnSprintStarted(InputAction.CallbackContext ctx)
        {
            SprintPressed = true;
        }

        private void OnJumpCanceled(InputAction.CallbackContext ctx)
        {
            JumpPressed = false;
        }

        private void OnJumpStarted(InputAction.CallbackContext ctx)
        {
            if (!JumpPressed)
            {
                OnJumpPressed?.Invoke();
            }

            JumpPressed = true;
        }

        private void OnInteractCanceled(InputAction.CallbackContext ctx)
        {
            InteractPressed = false;
        }

        private void OnInteractStarted(InputAction.CallbackContext ctx)
        {
            InteractPressed = true;
        }

        private void OnLookPerformed(InputAction.CallbackContext ctx)
        {
            LookInput = ctx.ReadValue<Vector2>();
        }

        private void OnMovePerformed(InputAction.CallbackContext ctx)
        {
            MoveInput = ctx.ReadValue<Vector2>();
        }

        private void OnEnable()
        {
            inputs.Enable();
        }

        private void OnDisable()
        {
            inputs.Disable();
        }


        /*
        [Header("Character Input Values")]
        public Vector2 move;

        public Vector2 look;
        public bool jump;
        public bool sprint;

        [Header("Movement Settings")]
        public bool analogMovement;

        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;
        public bool cursorInputForLook = true;


        public bool interactInput;

#if ENABLE_INPUT_SYSTEM
        public void OnMove(InputValue value)
        {
            MoveInput(value.Get<Vector2>());
        }

        public void OnLook(InputValue value)
        {
            if (cursorInputForLook)
            {
                LookInput(value.Get<Vector2>());
            }
        }

        public void OnJump(InputValue value)
        {
            JumpInput(value.isPressed);
        }

        public void OnSprint(InputValue value)
        {
            SprintInput(value.isPressed);
        }

        public void OnInteract(InputValue value)
        {
            interactInput = value.isPressed;
        }
#endif


        public void MoveInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection;
        }

        public void LookInput(Vector2 newLookDirection)
        {
            look = newLookDirection;
        }

        public void JumpInput(bool newJumpState)
        {
            jump = newJumpState;
        }

        public void SprintInput(bool newSprintState)
        {
            sprint = newSprintState;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            SetCursorState(cursorLocked);
        }

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }
        */
    }
}