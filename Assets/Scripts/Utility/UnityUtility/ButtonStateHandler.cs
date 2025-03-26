using UnityEngine;
using UnityEngine.UI;

namespace Ebleme.UI {

    public class ButtonStateHandler : MonoBehaviour {
        [SerializeField] private Image _buttonImage;

        private Color _normalColor;
        private bool? _currentState;
        private static readonly Color _disabledColor = new Color(0.3254902f, 0.3960784f, 0.4901961f, 1f);

        private void Awake() {
            _normalColor = _buttonImage.color;
        }

        public void OnInteractableStateChaged(bool isInteractable) {
            if (!_currentState.HasValue || _currentState.Value != isInteractable) {
                _currentState = isInteractable;
                _buttonImage.color = isInteractable ? _normalColor : _disabledColor;
            }
        }
    }
}
