// maebleme2

namespace Ebleme.MovementStrategy
{
    public class MovementStateMachine
    {
        private IMovementState currentState;

        public void SetState(IMovementState newState, PlayerMovement player)
        {
            currentState?.ExitState(player);
            currentState = newState;
            currentState.EnterState(player);
        }

        public void UpdateState(PlayerMovement player)
        {
            currentState?.UpdateState(player);
        }
    }
}