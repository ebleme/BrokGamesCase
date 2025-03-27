using UnityEngine;

namespace Ebleme.MovementStrategy
{
    public class JumpState : IMovementState
    {
        private float verticalVelocity;
        private const float gravity = 9.81f;
        private bool isJumping;

        public void EnterState(PlayerMovement player)
        {
            verticalVelocity = player.GetJumpPower();
            isJumping = true;
        }

        public void UpdateState(PlayerMovement player)
        {
            if (!isJumping) return;

            verticalVelocity -= gravity * Time.deltaTime;
            Vector3 moveDirection = new Vector3(player.InputHandler.MoveInput.x, 0.0f, player.InputHandler.MoveInput.y).normalized;

            if (player.InputHandler.MoveInput != Vector2.zero)
                moveDirection = player.transform.right * player.InputHandler.MoveInput.x + player.transform.forward * player.InputHandler.MoveInput.y;
            
            moveDirection.y = verticalVelocity;

            player.Move(moveDirection, player.GetMoveSpeed());

            if (player.IsGrounded && verticalVelocity < 0)
            {
                isJumping = false;
                player.ChangeState(new MoveState());
            }
        }

        public void ExitState(PlayerMovement player)
        {
            player.SetVerticalVelocity(0);
        }
    }
}