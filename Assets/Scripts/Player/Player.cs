// maebleme2

using System;
using Ebleme.Interactables;
using Ebleme.Models;
using Ebleme.ScrictableObjects;
using UnityEngine;
using Zenject;

namespace Ebleme
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private Transform cameraFollowPoint;

        private InteractableBase currentInteractableBase;

        [Inject]
        private GameManager gameManager;

        public void Set(PlayerPreset preset)
        {
            var playerMovement = GetComponent<PlayerMovement>();
            var upgradeData = gameManager.GetPlayerUpgradeData(preset.Id);

            if (upgradeData == null)
                upgradeData = new PlayerUpgradeData(preset.Id);


            playerMovement.SetUpgradeData(upgradeData);
            playerMovement.SetMoveSpeed(preset.moveSpeed);
            playerMovement.SetSprintSpeed(preset.sprintSpeed);
            playerMovement.SetJumpPower(preset.jumpPower);
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