using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Ebleme.Utility {

    [RequireComponent(typeof(CanvasScaler))]
    public class CanvasScalerRatioSetup : MonoBehaviour {
        [ShowInInspector, ReadOnly] private Vector2 _referenceResolution;

        private CanvasScaler _canvasScaler;

        private void Awake() {
            _canvasScaler = GetComponent<CanvasScaler>();
            _referenceResolution = _canvasScaler.referenceResolution;
            Init();
        }

        private void Init() {
            var fullHDRatio = _referenceResolution.y / _referenceResolution.x;
            var currentRatio = Screen.height / (Screen.width * 1f);
            _canvasScaler.matchWidthOrHeight = currentRatio < fullHDRatio ? 1f : 0f;
        }
    }
}
