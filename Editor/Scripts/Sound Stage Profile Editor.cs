using CinemaB;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SoundStageProfile))]
public class SoundStageProfileEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Get a reference to the target object (SoundStageProfile)
        SoundStageProfile profile = (SoundStageProfile)target;

        // Display the stage size enum as a dropdown
        profile.stageSize = (SoundStageProfile.StageSize)EditorGUILayout.EnumPopup("Stage Size", profile.stageSize);

        // Update size based on preset, and control whether it's editable
        if (profile.stageSize == SoundStageProfile.StageSize.Custom)
        {
            // Allow user to input custom size
            profile.customSize = EditorGUILayout.Vector3Field("Custom Size", profile.customSize);
            profile.size = profile.customSize; // Assign the custom size to the actual size
        }
        else
        {
            // Display the size as read-only (not editable)
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.Vector3Field("Size", profile.size);
            EditorGUI.EndDisabledGroup();
        }

        // Apply changes to the size based on the current preset
        profile.UpdateSize();

        // Mark the profile as dirty to save changes in the Inspector
        if (GUI.changed)
        {
            EditorUtility.SetDirty(profile);
        }
    }
}
