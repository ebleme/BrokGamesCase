using System.Collections.Generic;
using Ebleme.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Ebleme.Garage
{
    [CreateAssetMenu(fileName = "GameConfigs", menuName = "Ebleme/GameConfigs", order = 0)]
    public class GameConfigs : SingletonScriptableObject<GameConfigs>
    {
        [FoldoutGroup("General Configs")]
        [SerializeField]
        private float sceneTransitionDuration = 1f;

        public float SceneTransitionDuration => sceneTransitionDuration;
    }
}