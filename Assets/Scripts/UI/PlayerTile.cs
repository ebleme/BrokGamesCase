// maebleme2

using System;
using Ebleme.ScrictableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ebleme.UI
{
    public class PlayerTile : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text titleText;

        [SerializeField]
        private TMP_Text moveText;
        
        [SerializeField]
        private TMP_Text sprintText;
        
        [SerializeField]
        private TMP_Text jumpText;
        
        [SerializeField]
        private Button chooseButton;

        public void Set(PlayerPreset preset, Action onChoose)
        {
            titleText.text = preset.Id;
            moveText.text = preset.moveSpeed.ToString();
            sprintText.text = preset.sprintSpeed.ToString();
            jumpText.text = preset.jumpPower.ToString();
            
            chooseButton.onClick.AddListener(() => onChoose?.Invoke());
        }
    }
}