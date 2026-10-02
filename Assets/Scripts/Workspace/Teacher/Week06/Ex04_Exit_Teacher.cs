using Debug = Workspace.Core.SimpleDebugConsole;
using UnityEngine;
using Week06.Game;

namespace Week06.Teacher.Ex04
{
    public class Exit : MonoBehaviour
    {
        public int positionX;
        public int positionY;

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"[Trigger] {gameObject.name} collided with {other.gameObject.name}");

            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log("🎉 You Win! Reached the exit!");
            }
        }
    }
}
