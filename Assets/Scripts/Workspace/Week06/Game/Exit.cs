using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week06.Game
{
    public class Exit : MonoBehaviour
    {
        public int positionX;
        public int positionY;
        public MapGenerator mapGenerator;

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"[Trigger] {gameObject.name} collided with {other.gameObject.name}");
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log("You Win! Reached the exit!");
            }
        }
    }
}
