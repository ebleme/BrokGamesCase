using Ebleme;
using Ebleme.Models;
using Ebleme.MovementStrategy;
using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private MovementStateMachine stateMachine;

    private float jumpPower;
    private float moveSpeed;
    private float sprintSpeed;
    private float verticalVelocity;

    [Inject] private InputHandler inputHandler;

    private PlayerUpgradeData upgradeData;
    
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        stateMachine = new MovementStateMachine();

        stateMachine.SetState(new IdleState(), this);
    }

    private void Start()
    {
        inputHandler.OnJumpPressed += Jump;
    }

    private void OnDestroy()
    {
        inputHandler.OnJumpPressed -= Jump;
    }

    private void Update()
    {
        ApplyGravity();
        stateMachine.UpdateState(this);
    }

    public void SetUpgradeData(PlayerUpgradeData data) => upgradeData = data;
    public void SetMoveSpeed(float moveSpeed) => this.moveSpeed = moveSpeed;
    public void SetSprintSpeed(float sprintSpeed) => this.sprintSpeed = sprintSpeed;
    public void SetJumpPower(float jumpPower) => this.jumpPower = jumpPower;

    public float GetMoveSpeed() => moveSpeed * upgradeData.moveSpeedMultiplier;
    public float GetSprintSpeed() => sprintSpeed * upgradeData.sprintSpeedMultiplier;
    public float GetJumpPower() => jumpPower * upgradeData.jumpPowerMultiplier;

    public bool IsGrounded => characterController.isGrounded;
    public CharacterController Controller => characterController;
    public InputHandler InputHandler => inputHandler;

    private void Jump()
    {
        if (IsGrounded)
        {
            stateMachine.SetState(new JumpState(), this);
        }
    }

    private void ApplyGravity()
    {
        if (IsGrounded && verticalVelocity < 0.0f)
        {
            verticalVelocity = -2f;
        }
        else
        {
            verticalVelocity += GameConfigs.Instance.Gravity * Time.deltaTime;
        }
    }

    public void Move(Vector3 direction, float speed)
    {
        characterController.Move(direction * (speed * Time.deltaTime) + Vector3.up * (verticalVelocity * Time.deltaTime));
    }

    public void SetVerticalVelocity(float velocity) => verticalVelocity = velocity;
    
    
    public void ChangeState(IMovementState newState)
    {
        stateMachine.SetState(newState, this);
    }
}
