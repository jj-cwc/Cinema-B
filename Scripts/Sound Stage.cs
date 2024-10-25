using System;
using UnityEngine;

namespace CinemaB
{
    public class SoundStage : MonoBehaviour
    {
        public SoundStageProfile profile; // Reference to the SoundStageProfile
        private Vector3 lastKnownStageSize; // Tracks the last known size for change detection

        void Start()
        {
            if (profile == null)
            {
                Debug.LogError("No SoundStageProfile assigned! Please assign a profile to the Sound Stage.");
                return;
            }
            else
            {
                InitializeStage();
            }
        }

        // Called to set up or reset the stage based on the profile
        private void InitializeStage()
        {
            // Check if this is the first setup
            if (hasElement("Floor"))
            {
                ClearStage(); // Clear the stage if the profile size has changed
            }
            Debug.Log("Initializing stage with profile size: " + profile.size);
            AddSurface(Vector3.zero, new Vector3(profile.size.x, 0.1f, profile.size.z), "Floor");
            AddSurface(new Vector3(0, profile.size.y, 0), new Vector3(profile.size.x, 0.1f, profile.size.z), "Ceiling");
            AddSurface(new Vector3(0, profile.size.y / 2, -profile.size.z / 2), new Vector3(profile.size.x, profile.size.y, 0.1f), "Back Wall");
            if (profile.leftWall)
            {
                AddSurface(new Vector3(-profile.size.x / 2, profile.size.y / 2, 0),
                           new Vector3(0.1f, profile.size.y, profile.size.z), "Left Wall");
            }
            if (profile.rightWall)
            {
                AddSurface(new Vector3(profile.size.x / 2, profile.size.y / 2, 0),
                           new Vector3(0.1f, profile.size.y, profile.size.z), "Right Wall");
            }
            if (profile.frontWall)
            {
                AddSurface(new Vector3(0, profile.size.y / 2, profile.size.z / 2),
                           new Vector3(profile.size.x, profile.size.y, 0.1f), "Front Wall");
            }

            lastKnownStageSize = profile.size; // Update the last known size
        }

        private Boolean hasElement(string label)
        {
            SoundStageElement element = null;
            foreach (Transform child in transform)
            {
                element = isSoundStageElement(child);
                if (element && element.HasLabel(label))
                {
                    return true;
                }
            }
            return false;
        }

        // Destroy all stage elements
        public void ClearStage()
        {
            foreach (Transform child in transform)
            {
                if (isSoundStageElement(child))
                {
                    DestroyImmediate(child.gameObject);
                }
            }
            Debug.Log("Cleared stage elements.");
        }

        // Add a surface to the stage
        public void AddSurface(Vector3 position, Vector3 size, string label)
        {
            GameObject surface = GameObject.CreatePrimitive(PrimitiveType.Cube);
            surface.name = label;
            surface.transform.localScale = size; // Scale appropriately
            surface.transform.SetParent(transform);
            surface.transform.localPosition = position;
            SoundStageElement element = surface.AddComponent<SoundStageElement>();
            element.AddLabel(label);
            Debug.Log($"Added {label} at position {position} with size {size}");
        }

        // Update is called once per frame
        void Update()
        {
            if (profile != null && profile.size != lastKnownStageSize)
            {
                // Reconstruct the stage only if there is a profile change at runtime
                InitializeStage();
            }
        }

        // OnValidate to catch changes in the Inspector
        private void OnValidate()
        {
            if (profile != null && profile.size != lastKnownStageSize)
            {
                InitializeStage();
            }
        }

        private SoundStageElement isSoundStageElement(Transform transform)
        {
            return transform.gameObject.GetComponent<SoundStageElement>();
        }
    }
}
