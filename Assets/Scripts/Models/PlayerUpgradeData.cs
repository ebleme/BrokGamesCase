// maebleme2

namespace Ebleme.Models
{
    public class PlayerUpgradeData
    {
        public string Id;
        public float moveSpeedMultiplier;
        public float sprintSpeedMultiplier;
        public float jumpPowerMultiplier;

        public PlayerUpgradeData()
        {
            moveSpeedMultiplier = 0;
            sprintSpeedMultiplier = 0;
            jumpPowerMultiplier = 0;
        }
    }
}