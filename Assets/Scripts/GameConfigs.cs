using System.Collections.Generic;
using Ebleme.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Ebleme
{
    [CreateAssetMenu(fileName = "GameConfigs", menuName = "Ebleme/GameConfigs", order = 0)]
    public class GameConfigs : SingletonScriptableObject<GameConfigs>
    {
        [FoldoutGroup("General Configs")]
        [SerializeField]
        private float sceneTransitionDuration = 1f;

        [FoldoutGroup("Interactable")]
        [SerializeField]
        private LayerMask interactableLayer;

        [FoldoutGroup("Interactable")]
        [SerializeField] 
        private float interactableDistance = 100f;


        [FoldoutGroup("Addressable")]
        [SerializeField]
        private string playerPresetsLabel = "PlayerPresets";
        
        
        public float SceneTransitionDuration => sceneTransitionDuration;

        public LayerMask InteractableLayer => interactableLayer;
        public float InteractableDistance => interactableDistance;

        public string PlayerPresetsLabel => playerPresetsLabel;

    }
}