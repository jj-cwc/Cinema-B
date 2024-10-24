using UnityEngine;

namespace CinemaB
{
    [CreateAssetMenu(fileName = "SoundStageProfile", menuName = "Cinema B/Sound Stage Profile")]
    public class SoundStageProfile : ScriptableObject
    {
        public enum StageSize { TV, MidSize, FeatureFilm }

        [Header("Stage Settings")]
        public StageSize stageSize;

        [Range(1, 4)]
        public int numberOfWalls = 3; // B-movie sets typically have 3 walls, the 4th being open for the camera

        public bool hasWildWalls = false; // Wild walls can be moved for different shots

        // You can add more properties as needed, such as lighting or set dressing
    }
}
