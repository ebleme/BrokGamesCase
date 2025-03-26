// maebleme2

using System;
using Ebleme.ScrictableObjects;
using Ebleme.Utility;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Ebleme
{
    public class GameManager : SerializedSingleton<GameManager>
    {
        [SerializeField]
        private CinemachineCamera cinemachineCamera;

        public Player CurrentPlayer { get; private set; }
        public bool cursorLocked = true;

        protected override void Awake()
        {
            DontDestroyOnLoad(this);
            base.Awake();
        }

        private void Start()
        {
        }

        // private void OnApplicationFocus(bool hasFocus)
        // {
        //     SetCursorState(cursorLocked);
        // }

        public void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }

        public void SetCurrentPlayer(PlayerPreset playerPreset, Action onLoaded)
        {
            Addressables.LoadAssetAsync<GameObject>(playerPreset.Id).Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    var pos = Vector3.zero;
                    if (CurrentPlayer != null)
                    {
                        pos = CurrentPlayer.transform.position;
                        Destroy(CurrentPlayer.gameObject);
                    }

                    SetCursorState(true);
                    var newPlayer = Instantiate(handle.Result);
                    newPlayer.transform.position = pos;
                    CurrentPlayer = newPlayer.GetComponent<Player>();

                    cinemachineCamera.Target.TrackingTarget = CurrentPlayer.CameraFollowPoint;

                    onLoaded?.Invoke();
                }
            };
        }
    }
}