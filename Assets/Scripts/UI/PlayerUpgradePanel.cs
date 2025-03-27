// maebleme2

using System;
using DG.Tweening;
using Ebleme.Models;
using TMPro;
using UnityEngine;
using Zenject;

namespace Ebleme.UI
{
    public class PlayerUpgradePanel : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text moveSpeedText;

        [SerializeField]
        private TMP_Text sprintSpeedText;
        
        [SerializeField]
        private TMP_Text jumpPowerSpeedText;
        
        [Space]
        [SerializeField]
        private Canvas canvas;

        [SerializeField]
        private CanvasGroup canvasGroup;

        [Inject]
        private GameManager gameManager;
        
        private void Start()
        {
        }

        public void Show()
        {
            canvasGroup.alpha = 0;
            canvas.gameObject.SetActive(true);

            canvasGroup.DOFade(1, 0.25f);
            
            gameManager.SetCursorState(false);
            
            
            // Write Upgrade data
            var currentUpgradeData = GetActiveUpgradeData();

            if (currentUpgradeData == null)
                currentUpgradeData = new PlayerUpgradeData(gameManager.CurrentPlayerPreset.Id);
            
            
            moveSpeedText.text = currentUpgradeData.moveSpeedMultiplier.ToString();
            sprintSpeedText.text = currentUpgradeData.sprintSpeedMultiplier.ToString();
            jumpPowerSpeedText.text = currentUpgradeData.jumpPowerMultiplier.ToString();
        }

        public void Hide()
        {
            canvasGroup.alpha = 1;

            canvasGroup.DOFade(0, 0.25f).OnComplete(() =>
            {
                canvas.gameObject.SetActive(false);
                gameManager.SetCursorState(true);
            });
        }
        
        private PlayerUpgradeData GetActiveUpgradeData()
        {
            var upgradeData = gameManager.GetCurrrentPlayeerUpgradeData();
            
            if (upgradeData == null)
                upgradeData = new PlayerUpgradeData(gameManager.CurrentPlayerPreset.Id);

            return upgradeData;
        }
        
        public void UpgradeMoveSpeedMultiplier()
        {
            var currentUpgradeData = GetActiveUpgradeData();
            currentUpgradeData.moveSpeedMultiplier++;

            gameManager.UpgradePlayer(currentUpgradeData);
           
            Hide();
        }
        
        public void UpgradeSprintSpeed()
        {
            var currentUpgradeData = GetActiveUpgradeData();
            currentUpgradeData.sprintSpeedMultiplier++;
            
            gameManager.UpgradePlayer(currentUpgradeData);
           
            Hide();
        }
        
        public void UpgradeJumpPower()
        {
            var currentUpgradeData = GetActiveUpgradeData();
            currentUpgradeData.jumpPowerMultiplier++;
            
            gameManager.UpgradePlayer(currentUpgradeData);
            
            Hide();
        }
    }
}