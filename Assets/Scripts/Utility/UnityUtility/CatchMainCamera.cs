using UnityEngine;

namespace Ebleme.Utility {

    [RequireComponent(typeof(Canvas))]
    public class CatchMainCamera : MonoBehaviour {
        private Canvas _canvas;

        private void Awake() {
            _canvas = GetComponent<Canvas>();
            if (_canvas.worldCamera == null) {
                _canvas.worldCamera = Camera.main;
            }
        }
    }
}
