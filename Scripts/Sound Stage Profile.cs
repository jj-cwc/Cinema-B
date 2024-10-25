using System;
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

        public StageSize stageSize = StageSize.TV;
        public Vector3 size;
        public Vector3 customSize = new Vector3(30, 20, 50);
        public Boolean leftWall = false;
        public Boolean rightWall = false;
        public Boolean frontWall = false;

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