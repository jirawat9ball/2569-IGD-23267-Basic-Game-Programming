using Debug = Workspace.Core.SimpleDebugConsole;
using UnityEngine;
using Week06.Game;

namespace Week06.Teacher.HW01
{
    public class ItemSword : MonoBehaviour
    {
        public string Name = "Sword";
        public int attackBonus = 10;

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"[Trigger] {gameObject.name} collided with {other.gameObject.name}");

            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log($"⚔️ Picked up {Name}! +{attackBonus} Attack");
                player.IncreaseAttack(attackBonus);
                Destroy(gameObject);
            }
        }
    }
}
