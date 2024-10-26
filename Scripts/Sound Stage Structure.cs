using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CinemaB
{
    [ExecuteInEditMode]
    public class SoundStageStructure : MonoBehaviour
    {
        public enum StageSize { TV, MidSize, FeatureFilm, Custom }
        private enum StructureElement { Floor, Ceiling, BackWall, LeftWall, RightWall, FrontWall }

        [SerializeField] public StageSize stageSize = StageSize.TV;
        [SerializeField] public Vector3 customSize = new Vector3(30, 20, 50);
        [SerializeField] public bool leftWall = false;
        [SerializeField] public bool rightWall = false;
        [SerializeField] public bool frontWall = false;

        public Vector3 size;
        private Vector3 lastKnownStageSize;
        private Dictionary<StructureElement, GameObject> elements = new Dictionary<StructureElement, GameObject>();
        private Dictionary<StructureElement, bool> lastKnownWalls = new Dictionary<StructureElement, bool>
        {
            { StructureElement.LeftWall, false },
            { StructureElement.RightWall, false },
            { StructureElement.FrontWall, false }
        };

        void Start()
        {
            UpdateSize();
            InitializeElements();
            ConfigureElements();
        }

        public void UpdateSize()
        {
            size = stageSize switch
            {
                StageSize.TV => new Vector3(10, 8, 20),
                StageSize.MidSize => new Vector3(20, 15, 40),
                StageSize.FeatureFilm => new Vector3(50, 30, 100),
                StageSize.Custom => customSize,
                _ => size
            };
        }

        public void OnValidate()
        {
            UpdateSize();

            if (size != lastKnownStageSize ||
                lastKnownWalls[StructureElement.LeftWall] != leftWall ||
                lastKnownWalls[StructureElement.RightWall] != rightWall ||
                lastKnownWalls[StructureElement.FrontWall] != frontWall)
            {
                EditorApplication.update -= ConfigureOnNextEditorUpdate;
                EditorApplication.update += ConfigureOnNextEditorUpdate;
            }
        }

        private void ConfigureOnNextEditorUpdate()
        {
            ConfigureElements();
            EditorApplication.update -= ConfigureOnNextEditorUpdate;
        }

        private void InitializeElements()
        {
            GameObject structure = new GameObject("Structure");
            structure.transform.SetParent(transform);
            foreach (StructureElement elementType in System.Enum.GetValues(typeof(StructureElement)))
            {
                if (!elements.ContainsKey(elementType))
                {
                    elements[elementType] = CreateStructureElement(elementType, structure);
                    if (lastKnownWalls.ContainsKey(elementType))
                        lastKnownWalls[elementType] = false;
                } 
                else
                {
                    Debug.LogWarning("Element already exists: " + elementType);
                }
            }
        }

        private GameObject CreateStructureElement(StructureElement elementType, GameObject parent)
        {
            GameObject element = GameObject.CreatePrimitive(PrimitiveType.Cube);
            element.name = elementType.ToString();
            element.transform.parent = parent.transform;
            Renderer renderer = element.GetComponent<Renderer>();
            renderer.sharedMaterial.color = Color.white;
            return element;
        }

        private void UpdateLastKnownConfiguration()
        {
            lastKnownStageSize = size;
            lastKnownWalls[StructureElement.LeftWall] = leftWall;
            lastKnownWalls[StructureElement.RightWall] = rightWall;
            lastKnownWalls[StructureElement.FrontWall] = frontWall;
        }

        private void ConfigureElements()
        {
            foreach (KeyValuePair<StructureElement, GameObject> element in elements)
            {
                if (element.Value == null)
                {
                    Debug.LogWarning($"Skipping {element.Key} as it has not been initialized.");
                    continue;
                }

                element.Value.transform.localScale = GetElementDimensions(element.Key);
                element.Value.transform.localPosition = GetElementPosition(element.Key);

                element.Value.SetActive(element.Key switch
                {
                    StructureElement.Floor => true,
                    StructureElement.Ceiling => true,
                    StructureElement.BackWall => true,
                    StructureElement.LeftWall => leftWall,
                    StructureElement.RightWall => rightWall,
                    StructureElement.FrontWall => frontWall,
                    _ => false
                });
            }
            UpdateLastKnownConfiguration();
            UpdateLighting();
        }

        private void UpdateLighting()
        {
            var lighting = GetComponent<SoundStageLighting>();
            lighting?.StageSizeChanged(size);
        }

        private Vector3 GetElementDimensions(StructureElement elementType)
        {
            return elementType switch
            {
                StructureElement.Floor or StructureElement.Ceiling => new Vector3(size.x, 0.1f, size.z),
                StructureElement.BackWall => new Vector3(size.x, size.y, 0.1f),
                StructureElement.LeftWall or StructureElement.RightWall => new Vector3(0.1f, size.y, size.z),
                StructureElement.FrontWall => new Vector3(size.x, size.y, 0.1f),
                _ => Vector3.zero
            };
        }

        private Vector3 GetElementPosition(StructureElement elementType)
        {
            return elementType switch
            {
                StructureElement.Floor => new Vector3(0, -size.y / 2, 0),
                StructureElement.Ceiling => new Vector3(0, size.y / 2, 0),
                StructureElement.BackWall => new Vector3(0, 0, size.z / 2),
                StructureElement.LeftWall => new Vector3(-size.x / 2, 0, 0),
                StructureElement.RightWall => new Vector3(size.x / 2, 0, 0),
                StructureElement.FrontWall => new Vector3(0, 0, -size.z / 2),
                _ => Vector3.zero
            };
        }
    }
}
