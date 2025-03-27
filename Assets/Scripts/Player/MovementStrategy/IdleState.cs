// maebleme2

using UnityEngine;

namespace Ebleme.MovementStrategy
{
    public class IdleState : IMovementState
    {
        public void EnterState(PlayerMovement player)
        {
        }

        public void UpdateState(PlayerMovement player)
        {
            // Yalnızca hareket etmiyorsa Idle durumunda kalır
            if (player.InputHandler.MoveInput.sqrMagnitude > 0.01f)
            {
                player.ChangeState(new MoveState());
            }
        }

        public void ExitState(PlayerMovement player)
        {
        }
    }
}