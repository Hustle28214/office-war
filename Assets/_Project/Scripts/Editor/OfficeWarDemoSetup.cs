#if UNITY_EDITOR
using OfficeWar.Demo;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace OfficeWar.Editor
{
    public static class OfficeWarDemoSetup
    {
        const string ScenePath = "Assets/_Project/Scenes/Battle.unity";

        [MenuItem("Office War/Setup Demo Scene")]
        public static void SetupDemoScene()
        {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);

            var bootstrap = new GameObject("BattleDemo");
            bootstrap.AddComponent<BattleDemoBootstrap>();

            EditorSceneManager.SaveScene(scene, ScenePath);
            EditorBuildSettings.scenes = new[] { new EditorBuildSettingsScene(ScenePath, true) };
            Debug.Log($"Demo scene saved to {ScenePath}. Press Play to start.");
        }
    }
}
#endif
