using UnityEngine;
using Zenject;

namespace Ebleme
{
    public class PlayerMovement : MonoBehaviour
    {
        private float moveSpeed = 4.0f;
        private float sprintSpeed = 6.0f;

        [Space(10)]
        [SerializeField]
        private float jumpPower = 5f;

        [SerializeField]
        private float gravity = -15.0f;

        // player

        private float speed;
        private float verticalVelocity;


        private CharacterController characterController;

        private InputHandler inputHandler;
        
        [Inject]
        private void Construct(InputHandler inputHandler)
        {
            this.inputHandler = inputHandler;

            Debug.Log("zenject setup");
        }
        

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

        // private void GroundedCheck()
        // {
        //     // set sphere position, with offset
        //     // Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
        //     // grounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);
        //
        //   grounded =  Physics.Raycast(transform.position, Vector3.down, groundedOffset, groundLayers);
        // }

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
                verticalVelocity += gravity * Time.deltaTime;
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