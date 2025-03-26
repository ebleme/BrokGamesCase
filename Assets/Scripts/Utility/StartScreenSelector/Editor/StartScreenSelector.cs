// maebleme2

using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ebleme.Utility
{
    [InitializeOnLoad]
    public class StartScreenSelector : EditorWindow
    {
        private static string selectedScene;
        private static bool isEnabled;
        private static readonly string PrefKeyScene = "StartScreenSelector_SelectedScene";
        private static readonly string PrefKeyEnabled = "StartScreenSelector_Enabled";

        static StartScreenSelector()
        {
            EditorApplication.playModeStateChanged += OnPlayModeChanged;
        }

        [MenuItem("Tools/Start Screen Selector")]
        public static void ShowWindow()
        {
            GetWindow<StartScreenSelector>("Start Screen Selector");
        }

        private void OnEnable()
        {
            selectedScene = EditorPrefs.GetString(PrefKeyScene, "");
            isEnabled = EditorPrefs.GetBool(PrefKeyEnabled, false);
        }

        private void OnGUI()
        {
            isEnabled = EditorGUILayout.Toggle("Enable Start Screen", isEnabled);
            EditorPrefs.SetBool(PrefKeyEnabled, isEnabled);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Select Start Scene:", EditorStyles.boldLabel);

            if (!string.IsNullOrEmpty(selectedScene))
            {
                string selectedSceneName = Path.GetFileNameWithoutExtension(selectedScene);
                EditorGUILayout.LabelField($"Chosen Scene: {selectedSceneName}", EditorStyles.boldLabel);
            }

            var scenes = EditorBuildSettings.scenes
                .Where(scene => scene.enabled)
                .Select(scene => scene.path)
                .ToArray();

            foreach (var scene in scenes)
            {
                string sceneName = Path.GetFileNameWithoutExtension(scene);
                if (GUILayout.Button(sceneName))
                {
                    selectedScene = scene;
                    EditorPrefs.SetString(PrefKeyScene, selectedScene);
                }
            }
        }

        private static void OnPlayModeChanged(PlayModeStateChange state)
        {
            selectedScene = EditorPrefs.GetString(PrefKeyScene, "");
            isEnabled = EditorPrefs.GetBool(PrefKeyEnabled, false);

            if (!isEnabled || string.IsNullOrEmpty(selectedScene)) return;

            if (state == PlayModeStateChange.EnteredPlayMode)
            {
                if (!EditorSceneManager.GetActiveScene().path.Equals(selectedScene))
                {
                    try
                    {
                        ClearLog();

                        Debug.ClearDeveloperConsole(); // Console hatalarını temizle
                        SceneManager.LoadScene(selectedScene);
                    }
                    catch (System.Exception e)
                    {
                        Debug.LogError($"Failed to open scene: {selectedScene}. Error: {e.Message}");
                    }

                    // EditorSceneManager.OpenScene(selectedScene);
                }
            }
        }

        public static void ClearLog()
        {
            var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
            var type = assembly.GetType("UnityEditor.LogEntries");
            var method = type.GetMethod("Clear");
            method.Invoke(new object(), null);
        }
    }
}