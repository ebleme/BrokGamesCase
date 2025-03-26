// maebleme2

using System;
using System.Collections;
using DG.Tweening;
using Ebleme.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityUtils;

namespace Ebleme.Garage
{
    public class SceneLoader : SerializedSingleton<SceneLoader>
    {
        [SerializeField]
        private Image fadeImage;

        [SerializeField]
        private GameObject blocker;

        private bool _isFadeIn;
        private bool _isDuringTransition;
        public static Action<float> OnProgressChanged;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
            fadeImage.SetActive(false);
            blocker.SetActive(false);
        }

        public void LoadScene(int sceneIndex, Action onStart = null, Action onCompleted = null, float fadeOutDelay = 0f)
        {
            _isDuringTransition = false;
            StartCoroutine(LoadSceneCoroutine(sceneIndex, onStart, onCompleted, fadeOutDelay));
        }

        private IEnumerator LoadSceneCoroutine(int sceneIndex, Action onStart, Action onCompleted, float fadeOutDelay)
        {
            yield return null;
            blocker.SetActive(true);
            var sceneLoader = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single)!;

            sceneLoader.allowSceneActivation = false;
            sceneLoader.completed += _ =>
            {
                FadeOut(fadeOutDelay);
                onCompleted?.Invoke();
            };
            while (!sceneLoader.isDone)
            {
                OnProgressChanged?.Invoke(Mathf.Clamp01(sceneLoader.progress / 0.9f));
                if (sceneLoader.progress >= 0.9f)
                {
                    if (!_isDuringTransition)
                    {
                        yield return Constants.HalfSecondInterval;
                        _isDuringTransition = true;
                        FadeIn(() =>
                        {
                            onStart?.Invoke();
                            sceneLoader.allowSceneActivation = true;
                        });
                    }
                }

                yield return null;
            }
        }

        private void FadeIn(Action onComplete = null)
        {
            if (_isFadeIn)
            {
                return;
            }

            _isFadeIn = true;
            fadeImage.color = Color.clear;
            fadeImage.gameObject.SetActive(true);
            blocker.SetActive(true);
            fadeImage.DOColor(Color.black, Constants.HalfSecond).OnComplete(() => onComplete?.Invoke());
        }

        private void FadeOut(float delay, Action onComplete = null)
        {
            if (!_isFadeIn)
            {
                return;
            }

            if (delay <= 0)
            {
                _isFadeIn = false;
                fadeImage.DOColor(Color.clear, Constants.HalfSecond).OnComplete(() =>
                {
                    fadeImage.gameObject.SetActive(false);
                    blocker.SetActive(false);
                    onComplete?.Invoke();
                });
                return;
            }

            _isFadeIn = false;
            fadeImage.DOColor(Color.clear, Constants.HalfSecond)
                .SetDelay(delay)
                .OnComplete(() =>
                {
                    fadeImage.gameObject.SetActive(false);
                    blocker.SetActive(false);
                    onComplete?.Invoke();
                });
        }
    }
}