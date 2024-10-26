using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CinemaB
{

    [ExecuteInEditMode]
    public class SoundStageLighting : MonoBehaviour
    {
        public enum LightingPreset
        {
            StandardStage,
            OverheadLighting,
            Cinematic,
            Silhouette,
            HotSpots
        }

        [SerializeField] public GameObject focus;
        [SerializeField] private LightingPreset currentPreset;
        private Dictionary<string, Light> stageLights = new Dictionary<string, Light>();
        private Vector3 stageSize;

        void Start()
        {
            InitializeLights();
            ApplyLightingPreset();
        }

        private void InitializeLights()
        {
            if (stageLights.Count == 0)
            {
                GameObject lights = new GameObject("Lights");
                lights.transform.SetParent(transform);

                stageLights["KeyLight"] = CreateLight("KeyLight", LightType.Spot, new Vector3(2, 5, -3), Color.white, 1.0f, lights);
                stageLights["FillLight"] = CreateLight("FillLight", LightType.Spot, new Vector3(-2, 5, -3), Color.white, 0.5f, lights);
                stageLights["OverheadLight"] = CreateLight("OverheadLight", LightType.Spot, new Vector3(0, 10, 0), Color.yellow, 0.8f, lights);
                stageLights["BackLight"] = CreateLight("BackLight", LightType.Spot, new Vector3(0, 5, 3), Color.blue, 0.6f, lights);
            }
        }

        private Light CreateLight(string name, LightType type, Vector3 position, Color color, float intensity, GameObject parent)
        {
            GameObject lightGameObject = new GameObject(name);
            lightGameObject.transform.SetParent(parent.transform);
            lightGameObject.transform.localPosition = position;

            Light light = lightGameObject.AddComponent<Light>();
            light.type = type;
            light.color = color;
            light.intensity = intensity;
            light.enabled = false;

            return light;
        }

        public void StageSizeChanged(Vector3 size)
        {
            stageSize = size;
            UpdateLightPositions();
            // ApplyLightingPreset();
        }

        private void UpdateLightPositions()
        {
            stageLights["KeyLight"].transform.localPosition = new Vector3(stageSize.x / 4, stageSize.y - 0.5f, -stageSize.z / 2);
            stageLights["FillLight"].transform.localPosition = new Vector3(-stageSize.x / 4, stageSize.y - 0.5f, -stageSize.z / 2);
            stageLights["OverheadLight"].transform.localPosition = new Vector3(0, stageSize.y - 0.5f, 0);  // Near the ceiling
            stageLights["BackLight"].transform.localPosition = new Vector3(0, stageSize.y / 2, stageSize.z / 2);  // Midway for backlighting

            /*
            stageLights["KeyLight"].transform.localPosition = new Vector3(stageSize.x / 4, stageSize.y, -stageSize.z / 2);
            stageLights["FillLight"].transform.localPosition = new Vector3(-stageSize.x / 4, stageSize.y, -stageSize.z / 2);
            stageLights["OverheadLight"].transform.localPosition = new Vector3(0, stageSize.y * 1.5f, 0);
            stageLights["BackLight"].transform.localPosition = new Vector3(0, stageSize.y / 2, stageSize.z / 2);
            */

        }

        public void ApplyLightingPreset()
        {
            ResetAllLights();

            switch (currentPreset)
            {
                case LightingPreset.StandardStage:
                    stageLights["KeyLight"].enabled = true;
                    stageLights["FillLight"].enabled = true;
                    break;
                case LightingPreset.OverheadLighting:
                    stageLights["OverheadLight"].enabled = true;
                    break;
                case LightingPreset.Cinematic:
                    stageLights["KeyLight"].enabled = true;
                    stageLights["BackLight"].enabled = true;
                    break;
                case LightingPreset.Silhouette:
                    stageLights["BackLight"].enabled = true;
                    break;
                case LightingPreset.HotSpots:
                    stageLights["OverheadLight"].enabled = true;
                    stageLights["KeyLight"].enabled = true;
                    break;
            }
        }

        private void ResetAllLights()
        {
            foreach (Light light in stageLights.Values)
            {
                light.enabled = false;
            }
        }

        void OnValidate()
        {
            if (stageLights.Count == 0)
            {
                EditorApplication.update -= LightsNotReady;
                EditorApplication.update += LightsNotReady;
            }
            else
            {
                ApplyLightingPreset();
            }
        }

        private void LightsNotReady()
        {
            if (stageLights.Count == 0)
            {
                InitializeLights();
                EditorApplication.update -= LightsNotReady;
            }
            ApplyLightingPreset();
        }
    }
}