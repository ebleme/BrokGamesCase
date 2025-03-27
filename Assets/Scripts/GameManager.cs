using System.Collections.Generic;
using System.Linq;
using Ebleme.Models;
using Ebleme.SaveSystem;
using Ebleme.ScrictableObjects;
using Unity.Cinemachine;
using UnityEngine;

namespace Ebleme
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private CinemachineCamera cinemachineCamera;

        public Player CurrentPlayer { get; set; }
        public PlayerPreset CurrentPlayerPreset { get; set; }

        public void SetCursorState(bool isLocked)
        {
            Cursor.lockState = isLocked ? CursorLockMode.Locked : CursorLockMode.None;
        }

        public void SetCinemachineFollowTarget(Transform followTarget)
        {
            cinemachineCamera.Target.TrackingTarget = followTarget;
        }

        /// <summary>
        /// Sets Move Speed, Sprint Speed, Jump Power
        /// </summary>
        private void SetCurrentPlayerData()
        {
            CurrentPlayer.Set(CurrentPlayerPreset);
        }

        #region PlayerUpgrades

        public void UpgradePlayer(PlayerUpgradeData playerUpgradeData)
        {
            var upgradeData = SaveDataManager.SaveData.PlayerUpgrades.SingleOrDefault(p => p.id == playerUpgradeData.id);

            if (upgradeData != null)
                SaveDataManager.SaveData.PlayerUpgrades.Remove(upgradeData);

            SaveDataManager.SaveData.PlayerUpgrades.Add(playerUpgradeData);
            SaveDataManager.Save();
            
            SetCurrentPlayerData();
        }

        public PlayerUpgradeData GetPlayerUpgradeData(string id)
        {
            return SaveDataManager.SaveData.PlayerUpgrades.SingleOrDefault(p => p.id == id);
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