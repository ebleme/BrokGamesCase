// maebleme2

using System;
using DG.Tweening;
using Ebleme.Models;
using UnityEngine;

namespace Ebleme.UI
{
    public class PlayerUpgradePanel : MonoBehaviour
    {
        [SerializeField]
        private Canvas canvas;

        [SerializeField]
        private CanvasGroup canvasGroup;

        private void Start()
        {
            Hide();
        }

        public void Show()
        {
            canvasGroup.alpha = 0;
            canvas.gameObject.SetActive(true);

            canvasGroup.DOFade(1, 0.25f);
            
            GameManager.Instance.SetCursorState(false);
        }

        public void Hide()
        {
            canvasGroup.alpha = 1;

            canvasGroup.DOFade(0, 0.25f).OnComplete(() =>
            {
                canvas.gameObject.SetActive(true);
                GameManager.Instance.SetCursorState(true);
            });
        }
        
        private PlayerUpgradeData GetActiveUpgradeData()
        {
            var upgradeData = GameManager.Instance.GetCurrrentPlayeerUpgradeData();
            
            if (upgradeData == null)
                upgradeData = new PlayerUpgradeData();

            return upgradeData;
        }
        
        public void UpgradeMoveSpeedMultiplier()
        {
            var currentUpgradeData = GetActiveUpgradeData();
            currentUpgradeData.moveSpeedMultiplier++;
            
            GameManager.Instance.UpgradePlayer(currentUpgradeData);
        }
        
        public void UpgradeSprintSpeed()
        {
            var currentUpgradeData = GetActiveUpgradeData();
            currentUpgradeData.sprintSpeedMultiplier++;
            
            GameManager.Instance.UpgradePlayer(currentUpgradeData);
        }
        
        public void UpgradeJumpPower()
        {
            var currentUpgradeData = GetActiveUpgradeData();
            currentUpgradeData.jumpPowerMultiplier++;
            
            GameManager.Instance.UpgradePlayer(currentUpgradeData);
        }
    }
}