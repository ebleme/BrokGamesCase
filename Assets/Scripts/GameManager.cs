// maebleme2

using Ebleme.Utility;
using UnityEngine;

namespace Ebleme
{
    public class GameManager : SerializedSingleton<GameManager>
    {
        public bool cursorLocked = true;
        
        protected override void Awake()
        {
            DontDestroyOnLoad(this);
            base.Awake();
        }

        private void Start()
        {
            
        }
        
        private void OnApplicationFocus(bool hasFocus)
        {
            SetCursorState(cursorLocked);
        }

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }
}