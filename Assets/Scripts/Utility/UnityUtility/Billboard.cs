using UnityEngine;

namespace Ebleme.Utility {
    public class Billboard : MonoBehaviour {
        private Camera _cam;

        private void Awake() {
            _cam = Camera.main;
        }

        private void Start() {
            UpdateBillboard();
        }

        private void OnEnable() {
            UpdateBillboard();
        }

        protected void UpdateBillboard() {
            transform.LookAt(transform.position + _cam.transform.forward);
        }

        public virtual void SetBillboardState(bool state) {
            gameObject.SetActive(state);
        }
    }
}