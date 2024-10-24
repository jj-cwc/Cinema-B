using UnityEditor;
using UnityEngine;

namespace CinemaB
{
    public class SoundStageCreator
    {
        [MenuItem("GameObject/Cinema B/Create Sound Stage", false, 10)]
        public static void CreateSoundStage()
        {
            // Create a new GameObject
            GameObject soundStageGameObject = new GameObject("Sound Stage");

            // Attach the SoundStage component to the GameObject
            soundStageGameObject.AddComponent<SoundStage>();

            // Optionally, place the GameObject in the scene hierarchy
            Selection.activeGameObject = soundStageGameObject;
        }
    }
}
