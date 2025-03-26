// maebleme2

using Ebleme.Utility;

namespace Ebleme.Garage
{
    public class GameManager : SerializedSingleton<GameManager>
    {
        
        protected override void Awake()
        {
            DontDestroyOnLoad(this);
            base.Awake();
        }

        private void Start()
        {
            
        }
    }
}