// maebleme2

using System;
using System.Collections.Generic;
using System.Linq;
using Ebleme.Models;
using Ebleme.SaveSystem;
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
        public PlayerPreset CurrentPlayerPreset { get; private set; }
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
                    
                    CurrentPlayerPreset = playerPreset;
                    
                    CurrentPlayer = newPlayer.GetComponent<Player>();
                    CurrentPlayer.Set(playerPreset);

                    cinemachineCamera.Target.TrackingTarget = CurrentPlayer.CameraFollowPoint;

                    onLoaded?.Invoke();
                }
            };
        }

        #region PlayerUpgrades

        public void UpgradePlayer(PlayerUpgradeData playerUpgradeData)
        {
            var upgradeData = SaveDataManager.SaveData.PlayerUpgrades.SingleOrDefault(p => p.Id == playerUpgradeData.Id);

            if (upgradeData != null)
                SaveDataManager.SaveData.PlayerUpgrades.Remove(upgradeData);

            SaveDataManager.SaveData.PlayerUpgrades.Add(playerUpgradeData);
        }

        public PlayerUpgradeData GetPlayerUpgradeData(string id)
        {
            return SaveDataManager.SaveData.PlayerUpgrades.SingleOrDefault(p => p.Id == id);
        }
        
        public PlayerUpgradeData GetCurrrentPlayeerUpgradeData()
        {
            return GetPlayerUpgradeData(CurrentPlayerPreset.Id);
        }
        
        public List<PlayerUpgradeData> GetAllPlayeerUpgradeDatas()
        {
            return SaveDataManager.SaveData.PlayerUpgrades;
        }

        #endregion
    }
}