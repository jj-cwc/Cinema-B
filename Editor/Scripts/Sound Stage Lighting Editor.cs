using UnityEditor;
using UnityEngine;

namespace CinemaB
{
    [CustomEditor(typeof(SoundStageLighting))]
    public class SoundStageLightingEditor : Editor
    {
        SerializedProperty focusProp;
        SerializedProperty currentPresetProp;

        private void OnEnable()
        {
            // Fetch the properties
            focusProp = serializedObject.FindProperty("focus");
            currentPresetProp = serializedObject.FindProperty("currentPreset");
        }

        public override void OnInspectorGUI()
        {
            // Update the serialized object
            serializedObject.Update();

            // Draw the focus GameObject field
            EditorGUILayout.PropertyField(focusProp, new GUIContent("Focus Object"));

            // Draw the lighting preset dropdown
            currentPresetProp.enumValueIndex = (int)(SoundStageLighting.LightingPreset)EditorGUILayout.EnumPopup("Lighting Preset", (SoundStageLighting.LightingPreset)currentPresetProp.enumValueIndex);

            // Apply modified properties
            serializedObject.ApplyModifiedProperties();
        }
    }
}
