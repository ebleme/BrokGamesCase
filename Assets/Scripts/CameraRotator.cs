// maebleme2

using System;
using UnityEngine;
using Zenject;

namespace Ebleme
{
    public class CameraRotator : MonoBehaviour
    {
        [SerializeField]
        private float rotationSpeed = 1.0f;
        
        [Header("Cinemachine")]
        [SerializeField]
        private GameObject cinemachineCameraTarget;

        [SerializeField]
        public float cameraTopClamp = 90.0f;
        [SerializeField]
        public float cameraBottomClamp = -90.0f;
        
        private float cinemachineTargetPitch;
        private const float lookThreshold = 0.01f;
        private float rotationVelocity;

        [Inject]
        private InputHandler inputHandler;
        
        private void LateUpdate()
        {
            CameraRotation();
        }

        private void CameraRotation()
        {
            if (inputHandler.LookInput.sqrMagnitude >= lookThreshold)
            {
                cinemachineTargetPitch += inputHandler.LookInput.y * rotationSpeed * Time.deltaTime;
                rotationVelocity = inputHandler.LookInput.x * rotationSpeed * Time.deltaTime;

                cinemachineTargetPitch = ClampAngle(cinemachineTargetPitch, cameraBottomClamp, cameraTopClamp);
                cinemachineCameraTarget.transform.localRotation = Quaternion.Euler(cinemachineTargetPitch, 0.0f, 0.0f);

                transform.Rotate(Vector3.up * rotationVelocity);
            }
        }
        
        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
    }
}