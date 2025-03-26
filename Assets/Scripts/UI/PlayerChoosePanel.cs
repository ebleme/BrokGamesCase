// maebleme2

using System;
using System.Collections.Generic;
using Ebleme.ScrictableObjects;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Ebleme.UI
{
    public class PlayerChoosePanel : MonoBehaviour
    {
        [SerializeField]
        private PlayerTile tilePrefab;
        
        [SerializeField]
        private Transform content;

        private List<PlayerPreset> presets;

        void Start()
        {
            presets = new List<PlayerPreset>();
            LoadAllAssetsByLabel(GameConfigs.Instance.PlayerPresetsLabel);
            
            
        }

        void LoadAllAssetsByLabel(string label)
        {
            Addressables.LoadResourceLocationsAsync(label).Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    int totalAssets = handle.Result.Count;
                    int loadedCount = 0;
                    
                    foreach (var location in handle.Result)
                    {
                        Addressables.LoadAssetAsync<PlayerPreset>(location).Completed += assetHandle =>
                        {
                            if (assetHandle.Status == AsyncOperationStatus.Succeeded)
                            {
                                presets.Add(assetHandle.Result);
                                
                                loadedCount++;

                                // Tüm assetler yüklendiğinde event'i çağır.
                                if (loadedCount == totalAssets)
                                {
                                    ListPresets();
                                }
                            }
                        };
                    }
                }
            };
        }

        private void ListPresets()
        {
            foreach (Transform c in content)
                Destroy(c.gameObject);
            
            foreach (var preset in presets)
            {
                var tile = Instantiate(tilePrefab, content);
                tile.Set(preset, () => OnChoosed(preset));
            }
        }

        private void OnChoosed(PlayerPreset preset)
        {
            Debug.Log($"Choosed: {preset.Id}");
        }
    }
}