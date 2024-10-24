using UnityEngine;

namespace CinemaB
{
    public class SoundStage : MonoBehaviour
    {
        public SoundStageProfile profile; // The associated profile
        private SoundStageProfile lastProfile; // To track changes to the profile

        private const string SoundStageTag = "SoundStage";

        void Awake()
        {
            CreateTag(SoundStageTag);
            gameObject.tag = SoundStageTag;
            BuildStageFromProfile();
        }

        void Update()
        {
            // Check if the profile has been changed
            if (profile != lastProfile)
            {
                // If the profile has changed, destroy and rebuild the stage
                DestroyStage();
                BuildStageFromProfile();
                lastProfile = profile;
            }
        }

        public void BuildStageFromProfile()
        {
            if (profile == null)
            {
                Debug.LogWarning("No profile assigned to the Sound Stage.");
                return;
            }

            // Implement logic to build the stage from the profile
            Debug.Log("Building stage with profile: " + profile.name);
        }

        public void DestroyStage()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        private void CreateTag(string tagName)
        {
            if (!TagExists(tagName))
            {
                UnityEditor.SerializedObject tagManager = new UnityEditor.SerializedObject(UnityEditor.AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
                UnityEditor.SerializedProperty tagsProp = tagManager.FindProperty("tags");

                tagsProp.InsertArrayElementAtIndex(tagsProp.arraySize);
                tagsProp.GetArrayElementAtIndex(tagsProp.arraySize - 1).stringValue = tagName;
                tagManager.ApplyModifiedProperties();
            }
        }

        private bool TagExists(string tagName)
        {
            foreach (string tag in UnityEditorInternal.InternalEditorUtility.tags)
            {
                if (tag == tagName)
                    return true;
            }
            return false;
        }
    }
}
