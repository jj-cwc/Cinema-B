using UnityEngine;

namespace CinemaB
{
    public class SoundStage : MonoBehaviour
    {
        public SoundStageProfile profile; // Reference to the SoundStageProfile ScriptableObject

        // Start is called before the first frame update
        void Start()
        {
            if (profile != null)
            {
                // For example, set the GameObject's size based on the profile
                transform.localScale = profile.size;
            }
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}
