// maebleme2

using System;
using Ebleme.Interactables;
using Ebleme.Models;
using Ebleme.ScrictableObjects;
using UnityEngine;

namespace Ebleme
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private Transform cameraFollowPoint;
        
        private InteractableBase currentInteractableBase;


        public void Set(PlayerPreset preset)
        {
            var playerMovement = GetComponent<PlayerMovement>();
            var upgradeData = GameManager.Instance.GetPlayerUpgradeData(preset.Id);

            if (upgradeData == null)
                upgradeData = new PlayerUpgradeData(GameManager.Instance.CurrentPlayerPreset.Id);
            
            playerMovement.SetMoveSpeed(preset.moveSpeed * upgradeData.moveSpeedMultiplier);
            playerMovement.SetSprintSpeed(preset.sprintSpeed * upgradeData.sprintSpeedMultiplier);
            playerMovement.SetJumpHeight(preset.jumpPower * upgradeData.jumpPowerMultiplier);
        }
        
        private void Update()
        {
            CheckInteractable();
        }

        private void CheckInteractable()
        {
            var camTr = Camera.main.transform;
            var ray = new Ray(camTr.position, camTr.forward);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, GameConfigs.Instance.InteractableDistance, GameConfigs.Instance.InteractableLayer))
            {
                Debug.DrawRay(ray.origin, ray.direction * GameConfigs.Instance.InteractableDistance, Color.green);
                var interactableBase = hit.collider.GetComponent<InteractableBase>();
                if (interactableBase != null)
                {
                    if (currentInteractableBase != null & currentInteractableBase != interactableBase)
                        currentInteractableBase.Defocuse();

                    currentInteractableBase = interactableBase;
                    interactableBase.Focuse();
                }
            }
            else if (currentInteractableBase != null)
            {
                currentInteractableBase.Defocuse();
                Debug.DrawRay(ray.origin, ray.direction * GameConfigs.Instance.InteractableDistance, Color.red);
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * GameConfigs.Instance.InteractableDistance, Color.red);
            }
        }
        
        public Transform CameraFollowPoint => cameraFollowPoint;

    }
}