#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace CinemaB
{
    public class CreateSoundStage
    {
        [MenuItem("Tools/Cinema B/New Sound Stage")]
        public static void NewSoundStage()
        {
            // Create a new GameObject with the SoundStage component
            GameObject soundStage = new GameObject("New Sound Stage");
            soundStage.AddComponent<SoundStage>();

            // Place the object in the scene
            Selection.activeObject = soundStage;
        }

        [MenuItem("Assets/Create/Cinema B/Sound Stage")]
        public static void CreateSoundStageAsset()
        {
            // Create a prefab variant for the sound stage
            GameObject soundStage = new GameObject("New Sound Stage");
            soundStage.AddComponent<SoundStage>();

            string path = AssetDatabase.GenerateUniqueAssetPath("Assets/NewSoundStage.prefab");
            PrefabUtility.SaveAsPrefabAsset(soundStage, path);
            GameObject.DestroyImmediate(soundStage);
        }
    }
}
#endif
