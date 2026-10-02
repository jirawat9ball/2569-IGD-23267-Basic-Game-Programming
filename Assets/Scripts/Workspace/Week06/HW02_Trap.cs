using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;
using Week06.Game;

namespace Week06.HW02
{
    public class Trap : MonoBehaviour
    {
        public string Name = "Trap";
        public int damage = 5;

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"[Trigger] {gameObject.name} collided with {other.gameObject.name}");

            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log($"⚠️ Stepped on {Name}! Trapped for 1 turn (-{damage} Energy)");
                player.isTrapped = true;
                player.TakeDamage(damage);
            }
        }
    }
}
