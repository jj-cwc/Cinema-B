#if UNITY_EDITOR
using CinemaB;
using UnityEditor;
using UnityEngine;

namespace CinemaB
{
    public class CreateSoundStageProfile
    {
        [MenuItem("Tools/Cinema B/New Sound Stage Profile")]
        public static void NewSoundStageProfile()
        {
            // Get the current active folder in the project
            string path = "Assets";

            // Check if the user has selected a folder in the Project window
            if (Selection.activeObject != null)
            {
                path = AssetDatabase.GetAssetPath(Selection.activeObject);

                // If the selected object is a file, strip the filename to get the folder path
                if (!string.IsNullOrEmpty(path) && !AssetDatabase.IsValidFolder(path))
                {
                    path = System.IO.Path.GetDirectoryName(path);
                }
            }

            // Create a new SoundStageProfile asset
            SoundStageProfile profile = ScriptableObject.CreateInstance<SoundStageProfile>();
            AssetDatabase.CreateAsset(profile, AssetDatabase.GenerateUniqueAssetPath(path + "/New Sound Stage Profile.asset"));

            // Save the asset and focus on it in the Project window
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = profile;
        }
    }
#endif
}