using Debug = Workspace.Core.SimpleDebugConsole;
using UnityEngine;
using Week06.Game;

namespace Week06.Teacher.Ex05
{
    public class ItemPotion : MonoBehaviour
    {
        public string name = "Potion";
        public int healPoint = 10;

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"[Trigger] {gameObject.name} collided with {other.gameObject.name}");

            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log($"✨ Picked up {name}! +{healPoint} Energy");
                player.Heal(healPoint);
                Destroy(gameObject);
            }
        }
    }
}
