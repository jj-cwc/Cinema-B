using UnityEngine;

namespace CinemaB
{
    public class WildWall : MonoBehaviour
    {
        public void SetWildWall(bool isWild)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.isKinematic = !isWild; // Make walls movable if wild
        }
    }
}
