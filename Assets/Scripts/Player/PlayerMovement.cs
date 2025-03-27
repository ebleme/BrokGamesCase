using UnityEngine;
using Zenject;

namespace Ebleme
{
    public class PlayerMovement : MonoBehaviour
    {
        private float jumpPower { get; set; }
        private float moveSpeed { get; set; }
        private float sprintSpeed { get; set; }

        
        // Privates
        private float speed;
        private float verticalVelocity;


        private CharacterController characterController;

        [Inject]
        private InputHandler inputHandler;
        
        private void Start()
        {
            characterController = GetComponent<CharacterController>();

            // Todo: User inject
            // inputHandler = FindFirstObjectByType<InputHandler>();

            inputHandler.OnJumpPressed += Jump;
        }

        private void OnDestroy()
        {
            inputHandler.OnJumpPressed -= Jump;
        }

        private void Update()
        {
            // JumpAndGravity();
            ApplyGravity();
            Move();
        }

        private bool IsGrounded => characterController.isGrounded;

        private void Move()
        {
            var targetSpeed = inputHandler.SprintPressed ? sprintSpeed : moveSpeed;

            if (inputHandler.MoveInput == Vector2.zero) targetSpeed = 0.0f;

            var currentHorizontalSpeed = new Vector3(characterController.velocity.x, 0.0f, characterController.velocity.z).magnitude;

            var speedOffset = 0.1f;
            var inputMagnitude = inputHandler.MoveInput.magnitude;

            if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime);

                // round speed to 3 decimal places
                speed = Mathf.Round(speed * 1000f) / 1000f;
            }
            else
            {
                speed = targetSpeed;
            }

            // normalise input direction
            Vector3 inputDirection = new Vector3(inputHandler.MoveInput.x, 0.0f, inputHandler.MoveInput.y).normalized;

            // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is a move input rotate player when the player is moving
            if (inputHandler.MoveInput != Vector2.zero)
            {
                // move
                inputDirection = transform.right * inputHandler.MoveInput.x + transform.forward * inputHandler.MoveInput.y;
            }

            // move the player
            characterController.Move(inputDirection.normalized * (speed * Time.deltaTime) + new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime);
        }


        private void Jump()
        {
            if (!IsGrounded)
            {
                return;
            }

            verticalVelocity += jumpPower;
        }

        private void ApplyGravity()
        {
            if (IsGrounded && verticalVelocity < 0.0f)
            {
                verticalVelocity = -1f;
            }
            else
            {
                verticalVelocity += GameConfigs.Instance.Gravity * Time.deltaTime;
            }
        }

        public void SetMoveSpeed(float moveSpeed)
        {
            this.moveSpeed = moveSpeed;
        }

        public void SetSprintSpeed(float sprintSpeed)
        {
            this.sprintSpeed = sprintSpeed;
        }

        public void SetJumpPower(float jumpPower)
        {
            this.jumpPower = jumpPower;
        }
    }
}