using UnityEngine;
using Debug = Workspace.Core.SimpleDebugConsole;

namespace Week06.Game
{
    public class ItemPotion : MonoBehaviour
    {
        public string Name = "Potion";
        public int positionX;
        public int positionY;
        public MapGenerator mapGenerator;

        public int healPoint = 10;

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"[Trigger] {gameObject.name} collided with {other.gameObject.name}");

            // ใช้ GetComponent เพื่อเข้าถึงและเรียกใช้ความสามารถของ Player โดยตรง
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log($"You got {Name} : {healPoint}");
                player.Heal(healPoint);
                Destroy(gameObject);
            }
        }

        public void Hit()
        {
            Player player = (mapGenerator != null) ? mapGenerator.player : null;
            if (player != null)
            {
                Debug.Log($"You got {Name} : {healPoint}");
                player.Heal(healPoint);
            }
            Destroy(gameObject);
        }
    }
}

