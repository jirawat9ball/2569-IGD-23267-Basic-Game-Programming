using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week06.Game
{
    public class Exit : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            HandleCollision(other.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            HandleCollision(other.gameObject);
        }

        private void HandleCollision(GameObject other)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log("You Win! Reached the exit!");
            }
        }
    }
}
