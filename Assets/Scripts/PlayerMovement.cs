// maebleme2

using System;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Ebleme
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Player")]
        [Tooltip("Move speed of the character in m/s")]
        [SerializeField]
        private float moveSpeed = 4.0f;

        [Tooltip("Sprint speed of the character in m/s")]
        [SerializeField]
        private float sprintSpeed = 6.0f;

        [Tooltip("Rotation speed of the character")]
        [SerializeField]
        private float rotationSpeed = 1.0f;

        [Tooltip("Acceleration and deceleration")]
        [SerializeField]
        private float speedChangeRate = 10.0f;

        [Space(10)]
        [Tooltip("The height the player can jump")]
        [SerializeField]
        private float jumpHeight = 1.2f;


        [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
        [SerializeField]
        private float gravity = -15.0f;

        [Space(10)]
        [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
        [SerializeField]
        private float jumpTimeout = 0.5f;

        [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
        [SerializeField]
        private float fallTimeout = 0.15f;

        [Header("Player Grounded")]
        [Tooltip("If the character is grounded or not.")]
        [SerializeField]
        private bool grounded = true;

        [Tooltip("Useful for rough ground")]
        [SerializeField]
        private float groundedOffset = -0.14f;

        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        [SerializeField]
        private float groundedRadius = 0.5f;

        [Tooltip("What layers the character uses as ground")]
        [SerializeField]
        private LayerMask groundLayers;

        [Header("Cinemachine")]
        [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
        [SerializeField]
        private GameObject cinemachineCameraTarget;

        [Tooltip("How far in degrees can you move the camera up")]
        public float cameraTopClamp = 90.0f;

        [Tooltip("How far in degrees can you move the camera down")]
        public float cameraBottomClamp = -90.0f;

        // cinemachine
        private float cinemachineTargetPitch;

        // player
        private float speed;
        private float rotationVelocity;
        private float verticalVelocity;
        private float terminalVelocity = 53.0f;

        // timeout deltatime
        private float jumpTimeoutDelta;
        private float fallTimeoutDelta;

        private CharacterController _controller;
        private InputHandler inputHandler;
        private GameObject mainCamera;

        private const float lookThreshold = 0.01f;

       

        private void Awake()
        {
            if (mainCamera == null)
                mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
            
            // Todo: User inject
            inputHandler = FindFirstObjectByType<InputHandler>();

            // reset our timeouts on start
            jumpTimeoutDelta = jumpTimeout;
            fallTimeoutDelta = fallTimeout;
            
            inputHandler.OnJumpPressed += JumpAndGravity;
        }

        private void OnDestroy()
        {
            inputHandler.OnJumpPressed -= JumpAndGravity;
        }

        private void Update()
        {
            JumpAndGravity();
            GroundedCheck();
            Move();
        }

        private void LateUpdate()
        {
            CameraRotation();
        }

        private void GroundedCheck()
        {
            // set sphere position, with offset
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
            grounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);
        }

        private void CameraRotation()
        {
            if (inputHandler.LookInput.sqrMagnitude >= lookThreshold)
            {
                cinemachineTargetPitch += inputHandler.LookInput.y * rotationSpeed * Time.deltaTime;
                rotationVelocity = inputHandler.LookInput.x * rotationSpeed * Time.deltaTime;

                cinemachineTargetPitch = ClampAngle(cinemachineTargetPitch, cameraBottomClamp, cameraTopClamp);
                cinemachineCameraTarget.transform.localRotation = Quaternion.Euler(cinemachineTargetPitch, 0.0f, 0.0f);

                transform.Rotate(Vector3.up * rotationVelocity);
            }
        }

        private void Move()
        {
            var targetSpeed = inputHandler.SprintPressed ? sprintSpeed : moveSpeed;

            if (inputHandler.MoveInput == Vector2.zero) targetSpeed = 0.0f;

            var currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

            var speedOffset = 0.1f;
            var inputMagnitude = inputHandler.MoveInput.magnitude;

            if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * speedChangeRate);

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
            _controller.Move(inputDirection.normalized * (speed * Time.deltaTime) + new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime);
        }

        private void JumpAndGravity()
        {
            if (grounded)
            {
                fallTimeoutDelta = fallTimeout;

                if (verticalVelocity < 0.0f)
                {
                    verticalVelocity = -2f;
                }

                if (inputHandler.JumpPressed && jumpTimeoutDelta <= 0.0f)
                {
                    // the square root of H * -2 * G = how much velocity needed to reach desired height
                    verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
                }

                if (jumpTimeoutDelta >= 0.0f)
                {
                    jumpTimeoutDelta -= Time.deltaTime;
                }
            }
            else
            {
                jumpTimeoutDelta = jumpTimeout;

                if (fallTimeoutDelta >= 0.0f)
                {
                    fallTimeoutDelta -= Time.deltaTime;
                }

                inputHandler.JumpPressed = false;
            }

            if (verticalVelocity < terminalVelocity)
            {
                verticalVelocity += gravity * Time.deltaTime;
            }
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (grounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
            Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z), groundedRadius);
        }
    }
}