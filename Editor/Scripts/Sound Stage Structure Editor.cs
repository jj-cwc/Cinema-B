using UnityEditor;
using UnityEngine;

namespace CinemaB
{
    [CustomEditor(typeof(SoundStageStructure))]
    public class SoundStageStructureEditor : Editor
    {
        SerializedProperty stageSizeProp;
        SerializedProperty customSizeProp;
        SerializedProperty leftWallProp;
        SerializedProperty rightWallProp;
        SerializedProperty frontWallProp;

        void OnEnable()
        {
            // Link serialized properties
            stageSizeProp = serializedObject.FindProperty("stageSize");
            customSizeProp = serializedObject.FindProperty("customSize");
            leftWallProp = serializedObject.FindProperty("leftWall");
            rightWallProp = serializedObject.FindProperty("rightWall");
            frontWallProp = serializedObject.FindProperty("frontWall");
        }

        public override void OnInspectorGUI()
        {
            // Update serialized object
            serializedObject.Update();

            // Display the stage size enum as a dropdown
            EditorGUILayout.PropertyField(stageSizeProp);

            // Update size based on preset, and control whether it's editable
            SoundStageStructure structure = (SoundStageStructure)target;
            if (structure.stageSize == SoundStageStructure.StageSize.Custom)
            {
                EditorGUILayout.PropertyField(customSizeProp, new GUIContent("Custom Size"));
                structure.size = structure.customSize; // Assign the custom size to the actual size
            }
            else
            {
                // Display the size as read-only (not editable)
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.Vector3Field("Size", structure.size);
                EditorGUI.EndDisabledGroup();
            }

            // Apply changes to the size based on the current preset
            structure.UpdateSize();

            // Display the wall toggle fields
            EditorGUILayout.Space(); // Add some space for clarity
            EditorGUILayout.LabelField("Wall Options", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(leftWallProp);
            EditorGUILayout.PropertyField(rightWallProp);
            EditorGUILayout.PropertyField(frontWallProp);

            // Apply property modifications
            serializedObject.ApplyModifiedProperties();

            // Mark the profile as dirty to save changes in the Inspector
            if (GUI.changed)
            {
                EditorUtility.SetDirty(structure);
                structure.OnValidate(); // Ensure OnValidate is called
            }
        }
    }
}
