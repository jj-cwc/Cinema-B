using System;
using System.Collections.Generic;
using UnityEngine;

namespace CinemaB
{
    public class SoundStageElement : MonoBehaviour
    {
        public List<string> labels = new List<string>(); // List of labels

        /*
        public void Awake()
        {
            if (!labels.Contains("Sound Stage Element"))
            {
                labels.Add("Sound Stage Element");
            }
            else
            {
                Debug.LogWarning("Sound Stage Element already contains the label 'Sound Stage Element'.");
            }
        }
        */

        public void AddLabel(string label)
        {
            label = label.ToLower();
            if (!labels.Contains(label))
            {
                labels.Add(label);
            }
            else
            {
                Debug.LogWarning("Sound Stage Element already contains the label '" + label + "'.");
            }
        }

        public void RemoveLabel(string label) 
        {
            label = label.ToLower();
            if (labels.Contains(label))
                {
                labels.Remove(label);
            }
            else
            {
                Debug.LogWarning("Sound Stage Element does not contain the label '" + label + "'.");
            }
        }

        public Boolean HasLabel(string label) {
            label = label.ToLower();
            return labels.Contains(label);
        }
    }
}

