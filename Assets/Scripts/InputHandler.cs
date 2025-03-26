// maebleme2

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
        public bool SprintPressed { get; private set; }

        private void Awake()
        {
            inputs = new PlayerInputs();
        
            // Hareket
            inputs.Player.Move.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
            inputs.Player.Move.canceled += ctx => MoveInput = Vector2.zero;

            // Bakış (Mouse veya Gamepad Right Stick)
            inputs.Player.Look.performed += ctx => LookInput = ctx.ReadValue<Vector2>();
            inputs.Player.Look.canceled += ctx => LookInput = Vector2.zero;

            // Etkileşim (E veya Gamepad X)
            inputs.Player.Interact.started += ctx => InteractPressed = true;
            inputs.Player.Interact.canceled += ctx => InteractPressed = false;

            // Zıplama (Space veya Gamepad A)
            inputs.Player.Jump.started += ctx => JumpPressed = true;
            inputs.Player.Jump.canceled += ctx => JumpPressed = false;  
            
            inputs.Player.Sprint.started += ctx => SprintPressed = true;
            inputs.Player.Sprint.canceled += ctx => SprintPressed = false;
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