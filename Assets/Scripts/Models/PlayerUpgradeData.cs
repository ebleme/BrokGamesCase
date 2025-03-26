// maebleme2

namespace Ebleme.Models
{
    public class PlayerUpgradeData
    {
        public string id;
        public float moveSpeedMultiplier;
        public float sprintSpeedMultiplier;
        public float jumpPowerMultiplier;

        public PlayerUpgradeData(string id)
        {
            this.id = id;
            moveSpeedMultiplier = 1;
            sprintSpeedMultiplier = 1;
            jumpPowerMultiplier = 1;
        }
    }
}