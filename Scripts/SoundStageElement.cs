using System.Collections.Generic;
using UnityEngine;

namespace CinemaB
{
    public class SoundStageElement : MonoBehaviour
    {
        // List of tags to represent multiple roles
        public List<string> tags = new List<string>();

        // Check if this element has a specific tag
        public bool HasTag(string tag)
        {
            return tags.Contains(tag);
        }

        // Add a tag to the element
        public void AddTag(string tag)
        {
            if (!tags.Contains(tag))
            {
                tags.Add(tag);
            }
        }

        // Remove a tag from the element
        public void RemoveTag(string tag)
        {
            if (tags.Contains(tag))
            {
                tags.Remove(tag);
            }
        }
    }
}
