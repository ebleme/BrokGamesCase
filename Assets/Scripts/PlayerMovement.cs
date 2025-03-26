// maebleme2

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
		private float jumpTimeout = 0.1f;

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


#if ENABLE_INPUT_SYSTEM
		private PlayerInput playerInput;
#endif
		private CharacterController _controller;
		private InputHandler inputHandler;
		private GameObject mainCamera;

		private const float lookThreshold = 0.01f;

		private bool IsCurrentDeviceMouse
		{
			get
			{
#if ENABLE_INPUT_SYSTEM
				return playerInput.currentControlScheme == "KeyboardMouse";
#else
				return false;
#endif
			}
		}

		private void Awake()
		{
			if (mainCamera == null)
				mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		}

		private void Start()
		{
			_controller = GetComponent<CharacterController>();
			inputHandler = GetComponent<InputHandler>();
#if ENABLE_INPUT_SYSTEM
			playerInput = GetComponent<PlayerInput>();
#else
			Debug.LogError( "Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif

			// reset our timeouts on start
			jumpTimeoutDelta = jumpTimeout;
			fallTimeoutDelta = fallTimeout;
		}

		private void FixedUpdate()
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
			if (inputHandler.look.sqrMagnitude >= lookThreshold)
			{
				//Don't multiply mouse input by Time.deltaTime
				float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

				cinemachineTargetPitch += inputHandler.look.y * rotationSpeed * deltaTimeMultiplier;
				rotationVelocity = inputHandler.look.x * rotationSpeed * deltaTimeMultiplier;

				// clamp our pitch rotation
				cinemachineTargetPitch = ClampAngle(cinemachineTargetPitch, cameraBottomClamp, cameraTopClamp);

				// Update Cinemachine camera target pitch
				cinemachineCameraTarget.transform.localRotation = Quaternion.Euler(cinemachineTargetPitch, 0.0f, 0.0f);

				// rotate the player left and right
				transform.Rotate(Vector3.up * rotationVelocity);
			}
		}

		private void Move()
		{
			float targetSpeed = inputHandler.sprint ? sprintSpeed : moveSpeed;

			if (inputHandler.move == Vector2.zero) targetSpeed = 0.0f;

			float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

			float speedOffset = 0.1f;
			float inputMagnitude = inputHandler.analogMovement ? inputHandler.move.magnitude : 1f;

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
			Vector3 inputDirection = new Vector3(inputHandler.move.x, 0.0f, inputHandler.move.y).normalized;

			// note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is a move input rotate player when the player is moving
			if (inputHandler.move != Vector2.zero)
			{
				// move
				inputDirection = transform.right * inputHandler.move.x + transform.forward * inputHandler.move.y;
			}

			// move the player
			_controller.Move(inputDirection.normalized * (speed * Time.fixedDeltaTime) + new Vector3(0.0f, verticalVelocity, 0.0f) * Time.fixedDeltaTime);
		}

		private void JumpAndGravity()
		{
			if (grounded)
			{
				// reset the fall timeout timer
				fallTimeoutDelta = fallTimeout;

				// stop our velocity dropping infinitely when grounded
				if (verticalVelocity < 0.0f)
				{
					verticalVelocity = -2f;
				}

				// Jump
				if (inputHandler.jump && jumpTimeoutDelta <= 0.0f)
				{
					// the square root of H * -2 * G = how much velocity needed to reach desired height
					verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
				}

				// jump timeout
				if (jumpTimeoutDelta >= 0.0f)
				{
					jumpTimeoutDelta -= Time.deltaTime;
				}
			}
			else
			{
				// reset the jump timeout timer
				jumpTimeoutDelta = jumpTimeout;

				// fall timeout
				if (fallTimeoutDelta >= 0.0f)
				{
					fallTimeoutDelta -= Time.fixedDeltaTime;
				}

				// if we are not grounded, do not jump
				inputHandler.jump = false;
			}

			// apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
			if (verticalVelocity < terminalVelocity)
			{
				verticalVelocity += gravity * Time.fixedDeltaTime;
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