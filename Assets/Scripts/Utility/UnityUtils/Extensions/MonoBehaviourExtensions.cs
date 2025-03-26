using System.Linq;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Ebleme.Utility;
using UnityEngine;

namespace UnityUtils {

    public static class MonoBehaviourExtensions {

        public static void SetLayerAllChildren(this Transform root, int layer) {
            var children = root.GetComponentsInChildren<Transform>(includeInactive: true);
            foreach (var child in children) {
                child.gameObject.layer = layer;
            }
        }

        public static void LockGroup(this CanvasGroup canvasGroup) {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        public static void LockCanvas(this CanvasGroup canvasGroup) {
            canvasGroup.interactable = false;
        }

        public static void UnlockGroup(this CanvasGroup canvasGroup) {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        public static void UnlockCanvas(this CanvasGroup canvasGroup) {
            canvasGroup.interactable = true;
        }

        public static void SetActive(this MonoBehaviour monoBehaviour, bool state) {
            monoBehaviour.gameObject.SetActive(state);
        }

        public static void SetParentActive(this MonoBehaviour obj, bool state) {
            obj.transform.parent.gameObject.SetActive(state);
        }

        public static bool CompareLayer(this Collider collider, int layer) {
            return collider.gameObject.layer == layer;
        }

        public static TweenerCore<float, float, FloatOptions> DOFillAmount(this SlicedFilledImage target, float endValue, float duration) {
            if (endValue > 1) endValue = 1;
            else if (endValue < 0) endValue = 0;
            TweenerCore<float, float, FloatOptions> t = DOTween.To(() => target.fillAmount, x => target.fillAmount = x, endValue, duration);
            t.SetTarget(target);
            return t;
        }

        public static bool IsCloseEnoughToTarget(this Transform transform, Vector3 target, float distance) {
            return (target - transform.position).sqrMagnitude < distance * distance;
        }

        public static bool IsFarEnoughToTarget(this Transform transform, Vector3 target, float distance) {
            return (target - transform.position).sqrMagnitude > distance * distance;
        }

        public static T[] OverlapSphereNonAlloc<T>(this Transform center, float radius, int layerMask) where T : Component {
            Collider[] hitColliders = new Collider[5];
            Physics.OverlapSphereNonAlloc(center.position, radius: radius, hitColliders, layerMask);
            return System.Array.FindAll(hitColliders, t => t != null).Select(t => t.GetComponent<T>()).ToArray();
        }
    }
}
