// maebleme2

using UnityEngine;

namespace Ebleme.MovementStrategy
{
    public class MoveState : IMovementState
    {
        public void EnterState(PlayerMovement player) { }

        public void UpdateState(PlayerMovement player)
        {
            Vector3 inputDirection = new Vector3(player.InputHandler.MoveInput.x, 0.0f, player.InputHandler.MoveInput.y).normalized;

            if (player.InputHandler.MoveInput != Vector2.zero)
            {
                inputDirection = player.transform.right * player.InputHandler.MoveInput.x + player.transform.forward * player.InputHandler.MoveInput.y;
            }
            
            if (inputDirection != Vector3.zero)
            {
                player.Move(inputDirection, player.GetMoveSpeed());

                if (player.InputHandler.SprintPressed)
                {
                    player.ChangeState(new SprintState());
                }
            }
            else
            {
                player.ChangeState(new IdleState());
            }
        }

        public void ExitState(PlayerMovement player) { }
    }

}