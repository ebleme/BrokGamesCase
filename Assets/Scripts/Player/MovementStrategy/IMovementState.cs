// maebleme2

namespace Ebleme.MovementStrategy
{
    public interface IMovementState
    {
        void EnterState(PlayerMovement player);
        void UpdateState(PlayerMovement player);
        void ExitState(PlayerMovement player);
    }
}