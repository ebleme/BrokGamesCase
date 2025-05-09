// maebleme2

using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using Ebleme.ScrictableObjects;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace Ebleme.UI
{
    public class PlayerChoosePanel : MonoBehaviour
    {
        [SerializeField]
        private PlayerTile tilePrefab;

        [SerializeField]
        private Transform content;

        [SerializeField]
        private Canvas canvas;

        [SerializeField]
        private CanvasGroup canvasGroup;

        private List<PlayerPreset> presets;

        private PlayerSpawner playerSpawner;
        private GameManager gameManager;

        [Inject]
        private void Construct(PlayerSpawner enemySpawner, GameManager gameManager)
        {
            this.playerSpawner = enemySpawner;
            this.gameManager = gameManager;
        }

        async void Start()
        {
            presets = new List<PlayerPreset>();
            LoadAllAssetsByLabel(GameConfigs.Instance.PlayerPresetsLabel);

            Show();
        }

        public void Show()
        {
            canvasGroup.alpha = 0;
            canvas.gameObject.SetActive(true);

            canvasGroup.DOFade(1, 0.25f);

            gameManager.SetCursorState(false);
        }

        public void Hide()
        {
            canvasGroup.alpha = 1;

            canvasGroup.DOFade(0, 0.25f).OnComplete(() =>
            {
                canvas.gameObject.SetActive(false);
                gameManager.SetCursorState(true);
            });
        }

        private void LoadAllAssetsByLabel(string label)
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

            playerSpawner.Spawn(preset, Hide);
        }
    }
}