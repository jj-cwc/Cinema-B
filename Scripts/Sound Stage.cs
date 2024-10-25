using UnityEditor;
using UnityEngine;

namespace CinemaB
{
    [ExecuteInEditMode]
    public class SoundStage : MonoBehaviour
    {
        public SoundStageProfile profile; // Reference to the SoundStageProfile
        private Vector3 lastKnownStageSize; // Tracks the last known size for change detection
        private bool needsInitialization = false; // Flag to defer initialization

        void Start()
        {
            if (profile == null)
            {
                Debug.LogError("No SoundStageProfile assigned! Please assign a profile to the Sound Stage.");
                return;
            }

            InitializeStage(); // Set up the stage once at runtime
        }

        // Called to set up or reset the stage based on the profile
        private void InitializeStage()
        {
            Debug.Log("Initializing stage with profile size: " + profile.size);
            ConfigureSurface(Vector3.zero, new Vector3(profile.size.x, 0.1f, profile.size.z), "Floor");
            ConfigureSurface(new Vector3(0, profile.size.y, 0), new Vector3(profile.size.x, 0.1f, profile.size.z), "Ceiling");
            ConfigureSurface(new Vector3(0, profile.size.y / 2, -profile.size.z / 2), new Vector3(profile.size.x, profile.size.y, 0.1f), "Back Wall");

            // Add walls based on profile settings
            ConfigureConditionalSurface(profile.leftWall, new Vector3(-profile.size.x / 2, profile.size.y / 2, 0),
                                        new Vector3(0.1f, profile.size.y, profile.size.z), "Left Wall");
            ConfigureConditionalSurface(profile.rightWall, new Vector3(profile.size.x / 2, profile.size.y / 2, 0),
                                        new Vector3(0.1f, profile.size.y, profile.size.z), "Right Wall");
            ConfigureConditionalSurface(profile.frontWall, new Vector3(0, profile.size.y / 2, profile.size.z / 2),
                                        new Vector3(profile.size.x, profile.size.y, 0.1f), "Front Wall");

            lastKnownStageSize = profile.size; // Update the last known size
            needsInitialization = false; // Reset flag after initialization
        }

        private void OnValidate()
        {
            if (profile != null && profile.HasChanged())
            {
                needsInitialization = true;
                EditorApplication.update += InitializeOnNextEditorUpdate;
            }
        }

        private void InitializeOnNextEditorUpdate()
        {
            if (needsInitialization)
            {
                InitializeStage();
            }
            EditorApplication.update -= InitializeOnNextEditorUpdate; // Remove the callback after initializing
        }

        // Adds or destroys surface based on profile settings
        private void ConfigureConditionalSurface(bool shouldExist, Vector3 position, Vector3 size, string label)
        {
            if (shouldExist)
            {
                ConfigureSurface(position, size, label);
            }
            else
            {
                DestroySurface(label);
            }
        }

        private void DestroySurface(string label)
        {
            Transform element = TryGetSurface(label);
            if (element != null)
            {
                DestroyImmediate(element.gameObject);
                Debug.Log($"Destroyed {label}");
            }
        }

        private Transform TryGetSurface(string label)
        {
            foreach (Transform child in transform)
            {
                SoundStageElement element = child.GetComponent<SoundStageElement>();
                if (element != null && element.HasLabel(label))
                {
                    return child;
                }
            }
            return null;
        }

        public void ConfigureSurface(Vector3 position, Vector3 size, string label)
        {
            Transform existingSurface = TryGetSurface(label);
            GameObject surface = existingSurface ? existingSurface.gameObject : GameObject.CreatePrimitive(PrimitiveType.Cube);

            surface.name = label;
            surface.transform.localScale = size;
            surface.transform.localPosition = position;
            surface.transform.SetParent(transform);

            if (!existingSurface)
            {
                SoundStageElement element = surface.AddComponent<SoundStageElement>();
                element.AddLabel(label);
                Debug.Log($"Added {label} at position {position} with size {size}");
            }
        }
    }
}
