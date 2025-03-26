using UnityEngine;

namespace Ebleme.Utility {
    public class Billboard : MonoBehaviour {
        private Camera _cam;

        private float speed = 10f;
            
        private void Awake() {
            _cam = Camera.main;
        }

        private void Start() {
            UpdateBillboard(1);
        }

        private void OnEnable() {
            UpdateBillboard(1);
        }

        protected void UpdateBillboard(float deltaTime) {
            transform.LookAt(transform.position + _cam.transform.forward * (deltaTime * speed));
        }

        public virtual void SetBillboardState(bool state) {
            gameObject.SetActive(state);
        }
    }
}