using UnityEditor;
using UnityEngine;

namespace CinemaB
{
    public class SoundStageCreator
    {
        [MenuItem("GameObject/Cinema B/Create Sound Stage", false, 10)]
        public static void CreateSoundStage()
        {
            GameObject soundStageGameObject = new GameObject("Sound Stage");
            soundStageGameObject.AddComponent<SoundStageLighting>();
            soundStageGameObject.AddComponent<SoundStageStructure>();
            Selection.activeGameObject = soundStageGameObject;
            soundStageGameObject.transform.position = Vector3.zero;
            soundStageGameObject.transform.localScale = Vector3.one;
            soundStageGameObject.transform.rotation = Quaternion.identity;
        }
    }
}
