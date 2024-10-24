#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace CinemaB
{
#if UNITY_EDITOR
    [CustomEditor(typeof(SoundStageProfile))]
    public class SoundStageProfileEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            SoundStageProfile profile = (SoundStageProfile)target;

            profile.stageSize = (SoundStageProfile.StageSize)EditorGUILayout.EnumPopup("Stage Size", profile.stageSize);
            profile.numberOfWalls = EditorGUILayout.IntSlider("Number of Walls", profile.numberOfWalls, 1, 4);
            profile.hasWildWalls = EditorGUILayout.Toggle("Wild Walls", profile.hasWildWalls);

            EditorUtility.SetDirty(profile); // Mark as dirty to save changes
        }
    }
#endif
}