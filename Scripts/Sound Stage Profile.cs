using UnityEngine;

namespace CinemaB
{
    [CreateAssetMenu(fileName = "SoundStageProfile", menuName = "CinemaB/Sound Stage Profile")]
    public class SoundStageProfile : ScriptableObject
    {
        public enum StageSize { TV, MidSize, FeatureFilm }
        public StageSize stageSize;

        public int numberOfWalls = 3; // Defaults to 3 walls
        public bool hasWildWalls = false;

        public Vector3 GetStageDimensions()
        {
            // Set the stage dimensions based on the selected size
            switch (stageSize)
            {
                case StageSize.TV:
                    return new Vector3(12.19f, 4.57f, 9.14f); // TV Stage: 40ft wide, 15ft tall, 30ft deep
                case StageSize.MidSize:
                    return new Vector3(18.29f, 7.62f, 15.24f); // Mid-Size Stage: 60ft wide, 25ft tall, 50ft deep
                case StageSize.FeatureFilm:
                    return new Vector3(30.48f, 12.19f, 24.38f); // Feature Film: 100ft wide, 40ft tall, 80ft deep
                default:
                    return Vector3.one; // Fallback, though this should never happen
            }
        }
    }
}
