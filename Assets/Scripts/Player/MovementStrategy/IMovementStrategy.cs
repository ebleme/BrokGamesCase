// maebleme2

using UnityEngine;

namespace Ebleme.MovementStrategy
{
    public interface IMovementStrategy
    {
        void Move(CharacterController characterController, Vector2 moveInput, bool isSprinting, Transform transform, ref float speed, ref float verticalVelocity);
        void Jump(ref float verticalVelocity, bool isGrounded, float jumpPower);
        void ApplyGravity(ref float verticalVelocity, bool isGrounded, float gravity);
    }
}