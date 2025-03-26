using Sirenix.OdinInspector;
using UnityEngine;

namespace Ebleme.Utility {

    public class ColliderSizeHandler : SerializedMonoBehaviour {
        [TabGroup("Box", Icon = SdfIconType.SquareFill), SerializeField] private Vector3 _boxOffset;
        [TabGroup("Box", Icon = SdfIconType.SquareFill), SerializeField] private Vector3 _boxSize;

        [TabGroup("Sphere", Icon = SdfIconType.CircleFill), SerializeField] private float _sphereRadius;
        [TabGroup("Sphere"), SerializeField] private Vector3 _sphereOffset;

        [SerializeField] private bool _drawGizmos;

        public (Vector3 offset, Vector3 size) GetBoxColliderSize() {
            return (_boxOffset, _boxSize);
        }

        public (Vector3 offset, float radius) GetSphereColliderSize() {
            return (_sphereOffset, _sphereRadius);
        }

#if UNITY_EDITOR

        private void OnDrawGizmos() {
            if (_drawGizmos) {
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(transform.position + _boxOffset, _boxSize);
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(transform.position + _sphereOffset, _sphereRadius);
            }
        }

#endif
    }
}
