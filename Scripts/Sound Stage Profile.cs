using UnityEngine;

[CreateAssetMenu(fileName = "SoundStageProfile", menuName = "Cinema B/Sound Stage Profile")]
public class SoundStageProfile : ScriptableObject
{
    public enum StageSize
    {
        TV,
        MidSize,
        FeatureFilm,
        Custom
    }

    public StageSize stageSize = StageSize.TV;
    public Vector3 size;
    public Vector3 customSize = new Vector3(30, 20, 50);
    public bool leftWall = true;
    public bool rightWall = true;
    public bool frontWall = true;

    private bool profileChanged = false; // Flag to signal profile changes

    // Automatically update size based on the selected preset
    public void UpdateSize()
    {
        switch (stageSize)
        {
            case StageSize.TV:
                size = new Vector3(10, 8, 20);
                break;
            case StageSize.MidSize:
                size = new Vector3(20, 15, 40);
                break;
            case StageSize.FeatureFilm:
                size = new Vector3(50, 30, 100);
                break;
            case StageSize.Custom:
                size = customSize;
                break;
        }
        profileChanged = true; // Mark as changed
    }

    private void OnValidate()
    {
        UpdateSize(); // Update size whenever profile values change in the Inspector
        profileChanged = true; // Signal a change to any listeners
    }

    // Method to check if the profile has changed
    public bool HasChanged()
    {
        bool hasChanged = profileChanged;
        profileChanged = false; // Reset flag after detecting change
        return hasChanged;
    }
}
