using UnityEngine;

namespace CinemaB
{
    [System.Serializable]
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

        // The selected stage size preset
        public StageSize stageSize = StageSize.TV;

        // The actual size of the stage, updated based on the preset
        public Vector3 size;

        // Custom size field
        public Vector3 customSize = new Vector3(30, 20, 50);

        // Preset sizes
        private static readonly Vector3 TVSize = new Vector3(10, 8, 20);
        private static readonly Vector3 MidSize = new Vector3(20, 15, 40);
        private static readonly Vector3 FeatureFilmSize = new Vector3(50, 30, 100);

        // Method to update the size based on the preset
        public void UpdateSize()
        {
            switch (stageSize)
            {
                case StageSize.TV:
                    size = TVSize;
                    break;
                case StageSize.MidSize:
                    size = MidSize;
                    break;
                case StageSize.FeatureFilm:
                    size = FeatureFilmSize;
                    break;
                case StageSize.Custom:
                    size = customSize;
                    break;
            }
        }
    }
}