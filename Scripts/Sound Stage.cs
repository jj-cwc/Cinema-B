using UnityEngine;

namespace CinemaB
{
    public class SoundStage : MonoBehaviour
    {
        public SoundStageProfile profile;
        private SoundStageProfile lastProfile;

        private const string SoundStageTag = "SoundStage";

        private GameObject floor;
        private GameObject[] walls;
        private GameObject ceiling;

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
                DestroyStage();
                BuildStageFromProfile();
                lastProfile = profile;
            }
        }

        // Build the sound stage based on the profile
        public void BuildStageFromProfile()
        {
            if (profile == null)
            {
                Debug.LogWarning("No profile assigned to the Sound Stage.");
                return;
            }

            Vector3 stageDimensions = profile.GetStageDimensions();

            // Build the floor
            BuildFloor(stageDimensions);

            // Build the walls
            BuildWalls(stageDimensions);

            // Build the ceiling
            BuildCeiling(stageDimensions);
        }

        private void BuildFloor(Vector3 stageDimensions)
        {
            if (floor != null)
                Destroy(floor);

            // Create a simple cube for the floor
            floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
            floor.name = "Floor";
            floor.transform.parent = transform;
            floor.transform.localScale = new Vector3(stageDimensions.x, 0.1f, stageDimensions.z); // Floor thickness 0.1
            floor.transform.position = new Vector3(0, 0, 0); // Floor at origin

            floor.tag = SoundStageTag;
        }

        private void BuildWalls(Vector3 stageDimensions)
        {
            // Remove existing walls
            if (walls != null)
            {
                foreach (GameObject wall in walls)
                {
                    if (wall != null)
                        Destroy(wall);
                }
            }

            walls = new GameObject[profile.numberOfWalls];

            float wallHeight = stageDimensions.y; // Use the Y dimension from the profile for wall height

            for (int i = 0; i < profile.numberOfWalls; i++)
            {
                // Create each wall and position it based on the number of walls
                walls[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                walls[i].name = "Wall " + (i + 1);
                walls[i].transform.parent = transform;
                walls[i].transform.localScale = new Vector3(stageDimensions.x, wallHeight, 0.1f); // Wall thickness 0.1

                // Positioning the walls around the stage
                switch (i)
                {
                    case 0: // Back wall
                        walls[i].transform.position = new Vector3(0, wallHeight / 2, -stageDimensions.z / 2);
                        break;
                    case 1: // Left wall
                        walls[i].transform.localScale = new Vector3(stageDimensions.z, wallHeight, 0.1f); // Side wall length
                        walls[i].transform.position = new Vector3(-stageDimensions.x / 2, wallHeight / 2, 0);
                        walls[i].transform.Rotate(0, 90, 0);
                        break;
                    case 2: // Right wall
                        walls[i].transform.localScale = new Vector3(stageDimensions.z, wallHeight, 0.1f);
                        walls[i].transform.position = new Vector3(stageDimensions.x / 2, wallHeight / 2, 0);
                        walls[i].transform.Rotate(0, -90, 0);
                        break;
                    case 3: // Front wall (optional for some configurations)
                        if (profile.numberOfWalls == 4)
                        {
                            walls[i].transform.position = new Vector3(0, wallHeight / 2, stageDimensions.z / 2);
                        }
                        break;
                }

                walls[i].tag = SoundStageTag;
            }
        }

        private void BuildCeiling(Vector3 stageDimensions)
        {
            if (ceiling != null)
                Destroy(ceiling);

            // Create a simple cube for the ceiling
            ceiling = GameObject.CreatePrimitive(PrimitiveType.Cube);
            ceiling.name = "Ceiling";
            ceiling.transform.parent = transform;
            ceiling.transform.localScale = new Vector3(stageDimensions.x, 0.1f, stageDimensions.z); // Ceiling thickness 0.1
            ceiling.transform.position = new Vector3(0, stageDimensions.y, 0); // Ceiling at the top

            ceiling.tag = SoundStageTag;
        }

        // Destroys the current stage
        public void DestroyStage()
        {
            // Destroy the floor, walls, and ceiling
            if (floor != null) Destroy(floor);
            if (walls != null)
            {
                foreach (var wall in walls)
                {
                    if (wall != null) Destroy(wall);
                }
            }
            if (ceiling != null) Destroy(ceiling);
        }

        // Helper method to create a tag if it doesn't exist
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

        // Helper method to check if a tag already exists
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
